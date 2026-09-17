import { useEffect, useState } from "react"
import { productService, type productList } from "../features/products/services/productService"

export default function HomePage() {
    const [data, setData] = useState<productList[]>([])

    useEffect(() => {
        async function carregarProdutos() {
            const produtos = await productService.getAll();

            if (produtos instanceof (Error))
                return;

            setData(produtos);
        }

        carregarProdutos();
    }, []);

    return (
        <>
            <h1>Home</h1>
            <ul>
                {data.map(d => (
                    <li key={d.name}>{d.name}</li>
                ))}
            </ul>
        </>
    )
}