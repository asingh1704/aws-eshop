import { useEffect, useState } from "react";
import { getCatalog } from "../services/authservice";

const Home = () => {
    const [catalog, setCatalog] = useState([]);
    const [error, setError] = useState(null);

    useEffect(()=>{
        const fetchCatalog = async () => {
            try {
              const data = await getCatalog();
              setCatalog(data);
            } catch (err) {
              setError('Failed to fetch catalog data');
            }
          };
      
          fetchCatalog();
    })
    return (<div>Hello You MF!!!
        <h1>Catalog</h1>
      <ul>
        {catalog.map((item, index) => (
          <li key={index}>{item.same} - Qty: {item.qty} - Size: {item.size}</li>
        ))}
      </ul>
    </div>)
}

export default Home;