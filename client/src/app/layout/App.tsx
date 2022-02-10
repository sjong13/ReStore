import { useState, useEffect } from "react";
import Catalog from "../../features/catalog/Catalog";
import { Product } from "../models/product";

function App() {
    const [products, setProducts] = useState<Product[]>([]);
    
    useEffect(() => {
        fetch('https://localhost:5001/api/products')
            .then(response => response.json())
            .then(data => setProducts(data))
    }, [])
    
    function addProduct() {
        setProducts(prevState => [...prevState, 
            {
                id: prevState.length + 101,
                name: 'product' + (prevState.length + 1), 
                price: (prevState.length * 100) + 100,
                brand: 'brand',
                description: 'description',
                pictureUrl: 'http://picsum.photos/200',
                quantityInStock: 1
            }])
    }
  return (
    <div>
      <h1 style={{ color: 'blue' }}>Re-Store</h1>
        <Catalog products = {products} addProduct={addProduct}/>
    </div>
  );
}

export default App;
