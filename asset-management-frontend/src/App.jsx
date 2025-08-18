import React, { useState, useEffect } from 'react';
import './App.css';
import logo from './assets/noc_logo.png';

function App() {
  const [primaryAsset, setPrimaryAsset] = useState(null);
  const [associatedAsset, setAssociatedAsset] = useState(null);
  const [documentToView, setDocumentToView] = useState(null);
  const [documentToView2D, setDocumentToView2D] = useState(null);
  const [searchTag, setSearchTag] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [searchResults, setSearchResults] = useState([]);
  const [formMessage, setFormMessage] = useState('');
  
  // New state for conditional rendering
  const [showCreateForm, setShowCreateForm] = useState(false);

  // New states for the form
  const [newAsset, setNewAsset] = useState({
    tagNumber: '',
    assetName: '',
    description: '',
    assetType: '',
    manufacturer: '',
    model: '',
    serialNumber: '',
    size: '',
    installationDate: '',
    status: '',
    validationStatus: '',
    lastMaintenance: '',
    site: '',
    deckPlatform: '',
    areaCode: '',
    system: '',
    facilitySection: '',
    functionalClassID: '',
    subsystem: '',
    cmmmsRequired: '',
    tagFormatID: '',
  });

  const [newAssetDocuments, setNewAssetDocuments] = useState([]);
  const [newAsset2DModels, setNewAsset2DModels] = useState([]);

  // useEffect for debounced search
  useEffect(() => {
    const fetchSearchResults = async () => {
      if (searchTag.length > 2) {
        try {
          const response = await fetch(`http://localhost:5062/api/Assets/ByPartialTag/${searchTag}`);
          if (response.ok) {
            const data = await response.json();
            setSearchResults(data);
            setPrimaryAsset(null);
            setAssociatedAsset(null);
            setDocumentToView(null);
            setDocumentToView2D(null);
          } else {
            setSearchResults([]);
          }
        } catch (err) {
          console.error("Error fetching partial tags:", err);
          setSearchResults([]);
        }
      } else {
        setSearchResults([]);
      }
    };

    const debounceTimer = setTimeout(fetchSearchResults, 300);
    
    return () => clearTimeout(debounceTimer);
  }, [searchTag]);

  // Function to fetch a single asset by tag
  const fetchAssetByTag = async (tagNumber, isPrimary = true) => {
    setLoading(true);
    setError(null);
    setSearchResults([]);
    setDocumentToView(null);
    setDocumentToView2D(null);
    
    try {
      const response = await fetch(`http://localhost:5062/api/Assets/${tagNumber}`);

      if (!response.ok) {
        throw new Error('Asset not found');
      }

      const data = await response.json();
      if (isPrimary) {
        setPrimaryAsset(data);
        setAssociatedAsset(null);
      } else {
        setAssociatedAsset(data);
      }
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  // Click handler for autocomplete results
  const handleSelectResult = (tagNumber) => {
    setSearchTag(tagNumber);
    fetchAssetByTag(tagNumber);
  };
  
  // Click handler for associated assets, documents, and 2D models
  const handleViewAssociatedItem = (item) => {
    setDocumentToView(null);
    setAssociatedAsset(null);
    setDocumentToView2D(null);

    if (item.associatedTagNumber) {
      fetchAssetByTag(item.associatedTagNumber, false);
    } else if (item.filePath && item.is2DModel) {
      setDocumentToView2D(`http://localhost:5062${item.filePath}`);
    } else if (item.filePath) {
      setDocumentToView(`http://localhost:5062${item.filePath}`);
    }
  };
  
  // Function to close the split views
  const closeAssociatedView = (type) => {
    if (type === 'asset') {
        setAssociatedAsset(null);
    } else if (type === 'document') {
        setDocumentToView(null);
    } else if (type === '2dmodel') {
        setDocumentToView2D(null);
    }
  }

  // Handle input changes for the form fields
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewAsset(prevAsset => ({
      ...prevAsset,
      [name]: value
    }));
  };

  // Handle file changes for the file inputs
  const handleFileChange = (e, fileType) => {
    if (fileType === 'documents') {
      setNewAssetDocuments(Array.from(e.target.files));
    } else if (fileType === '2dmodels') {
      setNewAsset2DModels(Array.from(e.target.files));
    }
  };
  
  // Handle form submission
  const handleFormSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setFormMessage('');

    // Create a FormData object to send multipart/form-data
    const formData = new FormData();

    // Append all text fields
    for (const key in newAsset) {
      if (newAsset[key]) {
        formData.append(key, newAsset[key]);
      }
    }

    // Append all document files
    newAssetDocuments.forEach((file) => {
      formData.append('AssetDocuments', file);
    });

    // Append all 2D model files
    newAsset2DModels.forEach((file) => {
      formData.append('Asset2DModels', file);
    });

    try {
      const response = await fetch('http://localhost:5062/api/Assets', {
        method: 'POST',
        body: formData,
      });

      if (response.ok) {
        setFormMessage('Asset created successfully!');
        // Automatically fetch and display the newly created asset
        fetchAssetByTag(newAsset.tagNumber);
        // Hide the form after submission
        setShowCreateForm(false);
        // Clear the form
        setNewAsset({
          tagNumber: '',
          assetName: '',
          description: '',
          assetType: '',
          manufacturer: '',
          model: '',
          serialNumber: '',
          size: '',
          installationDate: '',
          status: '',
          validationStatus: '',
          lastMaintenance: '',
          site: '',
          deckPlatform: '',
          areaCode: '',
          system: '',
          facilitySection: '',
          functionalClassID: '',
          subsystem: '',
          cmmmsRequired: '',
          tagFormatID: '',
        });
        setNewAssetDocuments([]);
        setNewAsset2DModels([]);
      } else {
        const errorData = await response.json();
        setFormMessage(`Error: ${JSON.stringify(errorData)}`);
      }
    } catch (error) {
      setFormMessage(`Error: ${error.message}`);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <div className="header-info-container">
          <div className="logo-container">
            <img src={logo} className="App-logo" alt="logo" />
          </div>
          <h1>Asset Management Dashboard</h1>
        </div>
        <div className="search-container">
          <input
            type="text"
            value={searchTag}
            onChange={(e) => setSearchTag(e.target.value)}
            placeholder="Search by Tag Number (e.g., TAG-001)"
          />
          <button onClick={() => { setSearchTag(''); setPrimaryAsset(null); setAssociatedAsset(null); setDocumentToView(null); setDocumentToView2D(null); setSearchResults([]); }} className="clear-button">Clear Search</button>
          
          {/* New Button to show the form */}
          <button onClick={() => setShowCreateForm(true)} className="create-button">Add New Asset</button>

          {searchResults.length > 0 && (
            <ul className="autocomplete-dropdown">
              {searchResults.map((asset, index) => (
                <li key={index} onClick={() => handleSelectResult(asset.tagNumber)}>
                  {asset.tagNumber} - {asset.assetName}
                </li>
              ))}
            </ul>
          )}
        </div>
      </header>

      <main>
        {loading && <p className="loading">Loading Asset...</p>}
        {error && <p className="error">{error}</p>}

        {/* Conditional rendering for the main content */}
        {!loading && !error && !showCreateForm && (
          <div className="asset-view-container">
            
            {primaryAsset && (
              <div className="asset-card primary-asset-card">
                <h3>Primary Asset:</h3>
                <div className="asset-details">
                  <h4>{primaryAsset.assetName} (Tag: {primaryAsset.tagNumber})</h4>
                  <p><strong>Description:</strong> {primaryAsset.description}</p>
                  <p><strong>Asset Type:</strong> {primaryAsset.assetType}</p>
                  <p><strong>Manufacturer:</strong> {primaryAsset.manufacturer}</p>
                  <p><strong>Model:</strong> {primaryAsset.model}</p>
                  <p><strong>Serial Number:</strong> {primaryAsset.serialNumber}</p>
                  <p><strong>Size:</strong> {primaryAsset.size}</p>
                  <p><strong>Age:</strong> {primaryAsset.age} years</p>
                  <p><strong>Installation Date:</strong> {new Date(primaryAsset.installationDate).toLocaleDateString()}</p>
                  <p><strong>Status:</strong> {primaryAsset.status}</p>
                  <p><strong>Validation Status:</strong> {primaryAsset.validationStatus}</p>
                  <p><strong>Last Maintenance:</strong> {new Date(primaryAsset.lastMaintenance).toLocaleDateString()}</p>
                  <p><strong>Site:</strong> {primaryAsset.site}</p>
                  <p><strong>Deck/Platform:</strong> {primaryAsset.deckPlatform}</p>
                  <p><strong>Area Code:</strong> {primaryAsset.areaCode}</p>
                  <p><strong>System:</strong> {primaryAsset.system}</p>
                  <p><strong>Facility Section:</strong> {primaryAsset.facilitySection}</p>
                  <p><strong>Functional Class ID:</strong> {primaryAsset.functionalClassID}</p>
                  <p><strong>Subsystem:</strong> {primaryAsset.subsystem}</p>
                  <p><strong>CMMMS Required:</strong> {primaryAsset.cmmmsRequired}</p>
                  <p><strong>Tag Format ID:</strong> {primaryAsset.tagFormatID}</p>
                  <p><strong>Created At:</strong> {new Date(primaryAsset.createdAt).toLocaleString()}</p>
                  <p><strong>Updated At:</strong> {new Date(primaryAsset.updatedAt).toLocaleString()}</p>
                  
                  {primaryAsset.assetRelationships?.length > 0 && (
                    <div className="associated-items-list">
                      <h5>Associated Assets:</h5>
                      <ul>
                        {primaryAsset.assetRelationships.map(item => (
                          <li key={item.id} onClick={() => handleViewAssociatedItem(item)}>
                            {item.associatedTagNumber} - Type: {item.relationshipType}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}

                  {primaryAsset.assetDocuments?.length > 0 && (
                    <div className="associated-items-list">
                      <h5>Documents:</h5>
                      <ul>
                        {primaryAsset.assetDocuments.map(doc => (
                          <li key={doc.id} onClick={() => handleViewAssociatedItem(doc)}>
                            {doc.filePath.split('/').pop()}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}

                  {primaryAsset.asset2DModels?.length > 0 && (
                    <div className="associated-items-list">
                      <h5>2D Models:</h5>
                      <ul>
                        {primaryAsset.asset2DModels.map(model => (
                          <li key={model.id} onClick={() => handleViewAssociatedItem({...model, is2DModel: true})}>
                            {model.filePath.split('/').pop()}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}
                  
                  {primaryAsset.asset3DModels?.length > 0 && (
                    <div className="associated-items-list">
                      <h5>3D Models:</h5>
                      <ul>
                        {primaryAsset.asset3DModels.map(model => (
                          <li key={model.id}>
                            {model.filePath.split('/').pop()}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}
                </div>
              </div>
            )}
            
            {(associatedAsset || documentToView) && (
              <div className="asset-card associated-asset-card">
                <div className="card-header">
                  <h3>
                    {associatedAsset ? 'Associated Asset' : 'Document'}
                    <button className="close-button" onClick={() => closeAssociatedView(associatedAsset ? 'asset' : 'document')}>X</button>
                  </h3>
                </div>
                
                {associatedAsset && (
                  <div className="asset-details">
                    <h4>{associatedAsset.assetName} (Tag: {associatedAsset.tagNumber})</h4>
                    <p><strong>Description:</strong> {associatedAsset.description}</p>
                    <p><strong>Asset Type:</strong> {associatedAsset.assetType}</p>
                    <p><strong>Manufacturer:</strong> {associatedAsset.manufacturer}</p>
                    <p><strong>Model:</strong> {associatedAsset.model}</p>
                    <p><strong>Serial Number:</strong> {associatedAsset.serialNumber}</p>
                    <p><strong>Size:</strong> {associatedAsset.size}</p>
                    <p><strong>Age:</strong> {associatedAsset.age} years</p>
                    <p><strong>Installation Date:</strong> {new Date(associatedAsset.installationDate).toLocaleDateString()}</p>
                    <p><strong>Status:</strong> {associatedAsset.status}</p>
                    <p><strong>Validation Status:</strong> {associatedAsset.validationStatus}</p>
                    <p><strong>Last Maintenance:</strong> {new Date(associatedAsset.lastMaintenance).toLocaleDateString()}</p>
                    <p><strong>Site:</strong> {associatedAsset.site}</p>
                    <p><strong>Deck/Platform:</strong> {associatedAsset.deckPlatform}</p>
                    <p><strong>Area Code:</strong> {associatedAsset.areaCode}</p>
                    <p><strong>System:</strong> {associatedAsset.system}</p>
                    <p><strong>Facility Section:</strong> {associatedAsset.facilitySection}</p>
                    <p><strong>Functional Class ID:</strong> {associatedAsset.functionalClassID}</p>
                    <p><strong>Subsystem:</strong> {associatedAsset.subsystem}</p>
                    <p><strong>CMMMS Required:</strong> {associatedAsset.cmmmsRequired}</p>
                    <p><strong>Tag Format ID:</strong> {associatedAsset.tagFormatID}</p>
                    <p><strong>Created At:</strong> {new Date(associatedAsset.createdAt).toLocaleString()}</p>
                    <p><strong>Updated At:</strong> {new Date(associatedAsset.updatedAt).toLocaleString()}</p>
                  </div>
                )}

                {documentToView && (
                  <div className="document-viewer">
                    <iframe src={documentToView} title="Document Viewer" />
                  </div>
                )}
              </div>
            )}
            
            {documentToView2D && (
                <div className="asset-card associated-2dmodel-card">
                    <div className="card-header">
                        <h3>
                            2D Model
                            <button className="close-button" onClick={() => closeAssociatedView('2dmodel')}>X</button>
                        </h3>
                    </div>
                    <div className="document-viewer">
                        <iframe src={documentToView2D} title="2D Model Viewer" />
                    </div>
                </div>
            )}
          </div>
        )}
        
        {primaryAsset === null && searchTag.length > 2 && searchResults.length === 0 && (
          <p>No Assets Found matching "{searchTag}"</p>
        )}

        {primaryAsset === null && searchTag.length === 0 && searchResults.length === 0 && !showCreateForm && (
          <p>Start typing to search for assets.</p>
        )}
        
        {/* === NEW ADD ASSET FORM (Conditional Render) === */}
        {showCreateForm && (
          <div className="add-asset-container">
            <div className="card-header">
                <h2>Add New Asset</h2>
                <button className="close-button" onClick={() => setShowCreateForm(false)}>X</button>
            </div>
            <form onSubmit={handleFormSubmit} className="add-asset-form">
              <div className="form-group">
                <label htmlFor="tagNumber">Tag Number:</label>
                <input type="text" id="tagNumber" name="tagNumber" value={newAsset.tagNumber} onChange={handleInputChange} required />
              </div>
              <div className="form-group">
                <label htmlFor="assetName">Asset Name:</label>
                <input type="text" id="assetName" name="assetName" value={newAsset.assetName} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="description">Description:</label>
                <textarea id="description" name="description" value={newAsset.description} onChange={handleInputChange}></textarea>
              </div>
              <div className="form-group">
                <label htmlFor="assetType">Asset Type:</label>
                <input type="text" id="assetType" name="assetType" value={newAsset.assetType} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="manufacturer">Manufacturer:</label>
                <input type="text" id="manufacturer" name="manufacturer" value={newAsset.manufacturer} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="model">Model:</label>
                <input type="text" id="model" name="model" value={newAsset.model} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="serialNumber">Serial Number:</label>
                <input type="text" id="serialNumber" name="serialNumber" value={newAsset.serialNumber} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="size">Size:</label>
                <input type="text" id="size" name="size" value={newAsset.size} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="installationDate">Installation Date:</label>
                <input type="date" id="installationDate" name="installationDate" value={newAsset.installationDate} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="status">Status:</label>
                <input type="text" id="status" name="status" value={newAsset.status} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="validationStatus">Validation Status:</label>
                <input type="text" id="validationStatus" name="validationStatus" value={newAsset.validationStatus} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="lastMaintenance">Last Maintenance:</label>
                <input type="date" id="lastMaintenance" name="lastMaintenance" value={newAsset.lastMaintenance} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="site">Site:</label>
                <input type="text" id="site" name="site" value={newAsset.site} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="deckPlatform">Deck/Platform:</label>
                <input type="text" id="deckPlatform" name="deckPlatform" value={newAsset.deckPlatform} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="areaCode">Area Code:</label>
                <input type="text" id="areaCode" name="areaCode" value={newAsset.areaCode} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="system">System:</label>
                <input type="text" id="system" name="system" value={newAsset.system} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="facilitySection">Facility Section:</label>
                <input type="text" id="facilitySection" name="facilitySection" value={newAsset.facilitySection} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="functionalClassID">Functional Class ID:</label>
                <input type="text" id="functionalClassID" name="functionalClassID" value={newAsset.functionalClassID} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="subsystem">Subsystem:</label>
                <input type="text" id="subsystem" name="subsystem" value={newAsset.subsystem} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="cmmmsRequired">CMMMS Required:</label>
                <input type="text" id="cmmmsRequired" name="cmmmsRequired" value={newAsset.cmmmsRequired} onChange={handleInputChange} />
              </div>
              <div className="form-group">
                <label htmlFor="tagFormatID">Tag Format ID:</label>
                <input type="text" id="tagFormatID" name="tagFormatID" value={newAsset.tagFormatID} onChange={handleInputChange} />
              </div>
              
              {/* File Uploads */}
              <div className="form-group">
                <label htmlFor="assetDocuments">Documents (PDF, etc.):</label>
                <input type="file" id="assetDocuments" name="assetDocuments" onChange={(e) => handleFileChange(e, 'documents')} multiple />
              </div>
              
              <div className="form-group">
                <label htmlFor="asset2DModels">2D Models (CAD, etc.):</label>
                <input type="file" id="asset2DModels" name="asset2DModels" onChange={(e) => handleFileChange(e, '2dmodels')} multiple />
              </div>
              
              <button type="submit" disabled={loading}>
                {loading ? 'Creating...' : 'Create Asset'}
              </button>
              <button type="button" onClick={() => setShowCreateForm(false)}>Cancel</button>
              
              {formMessage && <p>{formMessage}</p>}
              
            </form>
          </div>
        )}

      </main>
    </div>
  );
}

export default App;