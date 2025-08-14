import React, { useState, useEffect } from 'react';
import './App.css';
import logo from './assets/noc_logo.png';

function App() {
  const [primaryAsset, setPrimaryAsset] = useState(null);
  const [associatedAsset, setAssociatedAsset] = useState(null);
  const [documentToView, setDocumentToView] = useState(null); // New state for PDF split view
  const [searchTag, setSearchTag] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [searchResults, setSearchResults] = useState([]);

  // This useEffect hook triggers a partial search every time the searchTag changes
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
            setDocumentToView(null); // Clear document view on new search
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

  // This function fetches the full asset details for a selected tag
  const fetchAssetByTag = async (tagNumber, isPrimary = true) => {
    setLoading(true);
    setError(null);
    setSearchResults([]);
    setDocumentToView(null); // Clear document view when fetching a new asset

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

  const handleSelectResult = (tagNumber) => {
    setSearchTag(tagNumber);
    fetchAssetByTag(tagNumber);
  };
  
  // New handler for clicking an associated asset or document
  const handleViewAssociatedItem = (item) => {
    setDocumentToView(null); // Clear previous document view
    if (item.associatedTagNumber) { // It's an asset relationship
      fetchAssetByTag(item.associatedTagNumber, false);
    } else if (item.filePath) { // It's a document or model
      setAssociatedAsset(null); // Clear previous asset view
      setDocumentToView(`http://localhost:5062${item.filePath}`);
    }
  };
  
  const closeAssociatedView = () => {
    setAssociatedAsset(null);
    setDocumentToView(null);
  }

  return (
    <div className="App">
      <header className="App-header">
        <div className="logo-container">
          <img src={logo} className="App-logo" alt="logo" />
        </div>
        <h1>Asset Management Dashboard</h1>
        <div className="search-container">
          <input
            type="text"
            value={searchTag}
            onChange={(e) => setSearchTag(e.target.value)}
            placeholder="Search by Tag Number (e.g., TAG-001)"
          />
          <button onClick={() => { setSearchTag(''); setPrimaryAsset(null); setAssociatedAsset(null); setDocumentToView(null); setSearchResults([]); }} className="clear-button">Clear Search</button>
          
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

        {!loading && !error && (
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
                            {doc.tagNumber}
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
                          <li key={model.id} onClick={() => handleViewAssociatedItem(model)}>
                            {model.tagNumber}
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
                            {model.tagNumber}
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
                    <button className="close-button" onClick={closeAssociatedView}>X</button>
                  </h3>
                </div>
                
                {associatedAsset && (
                  <div className="asset-details">
                    <h4>{associatedAsset.assetName} (Tag: {associatedAsset.tagNumber})</h4>
                    <p><strong>Description:</strong> {associatedAsset.description}</p>
                    <p><strong>Asset Type:</strong> {associatedAsset.assetType}</p>
                    {/* ... other details for associatedAsset ... */}
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
          </div>
        )}
        
        {primaryAsset === null && searchTag.length > 2 && searchResults.length === 0 && (
          <p>No Assets Found matching "{searchTag}"</p>
        )}

        {primaryAsset === null && searchTag.length === 0 && searchResults.length === 0 && (
          <p>Start typing to search for assets.</p>
        )}
      </main>
    </div>
  );
}

export default App;