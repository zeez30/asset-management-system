import { useState, useEffect } from 'react';
import './App.css';
import logo from './assets/noc_logo.png'; 

function App()
{
  const [assets, setAssets] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState(''); // New state for search input
  const [selectedAsset, setSelectedAsset] = useState(null); // New state to hold the asset chosen for detail view

  const API_BASE_URL = 'http://localhost:5062/api';

  // Function to fetch all assets (called on initial load)
  useEffect(() =>
    {
    const fetchAllAssets = async () =>
      {
      try
      {
        const response = await fetch(`${API_BASE_URL}/Assets`);
        if (!response.ok)
        {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setAssets(data.$values || []); // Access the $values array
      }
        catch (err)
      {
        console.error("Failed to fetch assets:", err);
        setError(err.message);
      }
        finally
      {
        setLoading(false);
      }
    };

    fetchAllAssets();
  }, []); // Empty dependency array means this runs once on mount

  // Function to handle asset search by tag number
  const handleSearch = async () => {
    if (!searchTerm) {
      // If search term is empty, maybe refetch all assets or clear selectedAsset
      setSelectedAsset(null); // Clear selected asset if search term is empty
      // You might also want to refetch all assets here if you want to show the full list again
      return;
    }
    setLoading(true);
    setError(null);
    try {
      // New API endpoint for searching by tag number (we will create this on backend next)
      const response = await fetch(`${API_BASE_URL}/Assets/ByTagNumber/${searchTerm}`);
      if (!response.ok) {
        // If 404, it means asset not found, which is not an error for the fetch operation
        if (response.status === 404) {
          setSelectedAsset(null); // No asset found for this tag
          setError(`Asset with Tag Number '${searchTerm}' not found.`);
        } else {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
      } else {
        const data = await response.json();
        // The API should ideally return a single asset object, not an array for ByTagNumber
        setSelectedAsset(data); // Assuming API returns a single object
        setAssets([]); // Clear the main assets list to show only the selected one
      }
    } catch (err) {
      console.error("Failed to search assets:", err);
      setError(err.message);
      setSelectedAsset(null);
    } finally {
      setLoading(false);
    }
  };


if(loading) return <div>Loading Assets...</div>;
// If there's an error and no selected asset (e.g., search failed)
if(error && !selectedAsset) return <div>Error: {error}</div>;

return (
  <div className="App">
    <img src={logo} className="logo" alt="logo"/>
    <h1>Asset Management Dashboard</h1>

    {/* Search functionality */}
    <div>
        <input
            type="text"
            placeholder="Search by Tag Number"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
        />
        <button onClick={handleSearch}>Search</button>
        {searchTerm && <button onClick={() => { setSearchTerm(''); setSelectedAsset(null); }}>Clear Search</button>}
    </div>

    <h2>Your Asset:</h2>
    {selectedAsset ? (
      <div className="asset-details-container">
        <h3>Details for: {selectedAsset.assetName} (Tag: {selectedAsset.tagNumber})</h3>
        
        <div className="asset-details-grid">
            <div className="detail-item"><strong>Description:</strong> <span>{selectedAsset.description}</span></div>
            <div className="detail-item"><strong>Type:</strong> <span>{selectedAsset.assetType}</span></div>
            <div className="detail-item"><strong>Manufacturer:</strong> <span>{selectedAsset.manufacturer}</span></div>
            <div className="detail-item"><strong>Model:</strong> <span>{selectedAsset.model}</span></div>
            <div className="detail-item"><strong>Serial Number:</strong> <span>{selectedAsset.serialNumber}</span></div>
            <div className="detail-item"><strong>Size:</strong> <span>{selectedAsset.size}</span></div>
            <div className="detail-item"><strong>Age:</strong> <span>{selectedAsset.age}</span></div>
            <div className="detail-item"><strong>Installation Date:</strong> <span>{selectedAsset.installationDate ? new Date(selectedAsset.installationDate).toLocaleDateString() : 'N/A'}</span></div>
            <div className="detail-item"><strong>Status:</strong> <span>{selectedAsset.currentStatus}</span></div>
            <div className="detail-item"><strong>Validation Status:</strong> <span>{selectedAsset.validationStatus}</span></div>
            <div className="detail-item"><strong>Maintenance:</strong> <span>{selectedAsset.lastMaintenanceDate ? `Last: ${new Date(selectedAsset.lastMaintenanceDate).toLocaleDateString()}` : 'N/A'} {selectedAsset.nextMaintenanceDate ? `| Next: ${new Date(selectedAsset.nextMaintenanceDate).toLocaleDateString()}` : ''}</span></div>
            
            {/* New fields from the model */}
            <div className="detail-item"><strong>Area Code:</strong> <span>{selectedAsset.areaCode}</span></div>
            <div className="detail-item"><strong>Deck/Platform:</strong> <span>{selectedAsset.deckPlatformCode}</span></div>
            <div className="detail-item"><strong>Facility Sector:</strong> <span>{selectedAsset.facilitySector}</span></div>
            <div className="detail-item"><strong>Service:</strong> <span>{selectedAsset.serviceDescription}</span></div>
            <div className="detail-item"><strong>Site:</strong> <span>{selectedAsset.site}</span></div>
            <div className="detail-item"><strong>System:</strong> <span>{selectedAsset.system}</span></div>
            <div className="detail-item"><strong>Subsystem:</strong> <span>{selectedAsset.subsystem}</span></div>
            <div className="detail-item"><strong>Functional Class ID:</strong> <span>{selectedAsset.functionalClassID}</span></div>
            <div className="detail-item"><strong>Tag Format ID:</strong> <span>{selectedAsset.tagFormatID}</span></div>
            <div className="detail-item"><strong>CMIMS Required:</strong> <span>{selectedAsset.cmimsRequired ? 'Yes' : 'No'}</span></div>
            
            {/* Boolean Flags Section */}
            <div className="detail-item full-width">
                <strong>Found In:</strong>
                <ul>
                    {selectedAsset.foundInADiagrams && <li>A-Diagrams</li>}
                    {selectedAsset.foundInADL && <li>ADL</li>}
                    {selectedAsset.foundInAEngineering && <li>A-Engineering</li>}
                    {selectedAsset.foundInAVEVAE3D && <li>AVEVA E3D</li>}
                    {selectedAsset.foundInAVEVAElectricalAndInstrumentation && <li>AVEVA E&I</li>}
                    {selectedAsset.foundInEDMS && <li>EDMS</li>}
                    {selectedAsset.foundInPiVision && <li>PiVision</li>}
                </ul>
            </div>
        </div>

        {/* --- Associated Items --- */}
        <h4>Associated Documents:</h4>
        {selectedAsset.documents && selectedAsset.documents.$values.length > 0 ? (
          <ul>
            {selectedAsset.documents.$values.map(doc => (
              <li key={doc.id}>{doc.fileName}</li>
            ))}
          </ul>
        ) : (
          <p>No documents found.</p>
        )}

        <h4>Associated 3D Models:</h4>
        {selectedAsset.threeDModels && selectedAsset.threeDModels.$values.length > 0 ? (
          <ul>
            {selectedAsset.threeDModels.$values.map(model => (
              <li key={model.id}>{model.fileName}</li>
            ))}
          </ul>
        ) : (
          <p>No 3D models found.</p>
        )}

        <h4>Associated 2D Drawings:</h4>
        {selectedAsset.twoDModels && selectedAsset.twoDModels.$values.length > 0 ? (
          <ul>
            {selectedAsset.twoDModels.$values.map(drawing => (
              <li key={drawing.id}>{drawing.fileName}</li>
            ))}
          </ul>
        ) : (
          <p>No 2D drawings found.</p>
        )}

      </div>
    ) : (
        // ... (rest of the code for displaying the list of all assets or "No Asset Found")
        // This part remains the same.
        Array.isArray(assets) && assets.length === 0 ? (
            <p>No Asset Found.</p>
        ) : (
            Array.isArray(assets) && (
                <ul>
                    {assets.map(assetItem => (
                        <li key={assetItem.tagNumber}>
                            <strong>{assetItem.assetName}</strong> (Tag: {assetItem.tagNumber}) - Type: {assetItem.assetType}
                        </li>
                    ))}
                </ul>
            )
        )
    )}
  </div>
);
}
export default App;