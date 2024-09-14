import { useEffect, useState } from "react";
import { getCatalog } from "../services/catalogservice";

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
        <h2>env is {process.env.NODE_ENV}</h2>
        <h2>uri is {process.env.REACT_APP_API_BASE_URL}</h2>
      <ul>
        {catalog.map((item, index) => (
          <li key={index}>{item.same} - Qty: {item.qty} - Size: {item.size}</li>
        ))}
      </ul>
    </div>)
}

export default Home;