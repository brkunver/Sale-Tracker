import { getAllProducts, getImageUrl } from "@/utils/ApiCalls/productApiCalls"
import { useState, useEffect } from "react"
import { Link } from "react-router-dom"
import { Input } from "../ui/input"
import { Product } from "@/types/productTypes"
import { Skeleton } from "../ui/skeleton"

export function SearchProducts() {
  const [inputValue, setInputValue] = useState("")
  const [debouncedValue, setDebouncedValue] = useState("")
  const [products, setProducts] = useState<Product[]>()
  const [loading, setLoading] = useState(false)
  const delay = 500 // 500 ms delay

  // Debounce effect
  useEffect(() => {
    setLoading(true)
    setProducts(undefined)
    if (inputValue == "") {
      setLoading(false)
    }
    const handler = setTimeout(() => {
      setDebouncedValue(inputValue)
    }, delay)

    return () => {
      clearTimeout(handler)
    }
  }, [inputValue])

  // Fetch products when debouncedValue changes
  useEffect(() => {
    if (debouncedValue) {
      async function fetchProducts() {
        let data = await getAllProducts({ name: debouncedValue })
        setLoading(false)
        setProducts(data.data)
      }

      fetchProducts()
    }
    setProducts(undefined)
  }, [debouncedValue])

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInputValue(e.target.value)
  }

  return (
    <section className="flex flex-col">
      <Input onChange={handleChange} value={inputValue} className="lg:min-w-[500px]" />
      <section id="found-products" className="flex flex-col gap-4 my-6">
        {products &&
          products.map((item) => (
            <Link to={`/product/${item.id}`} key={item.id} className="flex items-center gap-2 rounded border hover:outline outline-1 p-2 ">
              <img src={getImageUrl(item.imageUrl)} className="h-12 w-12 object-contain rounded-full"/>
              <p className="font-semibold">{item.name}</p>
            </Link>
          ))}
      </section>
      {loading && (
        <div className="flex flex-col gap-2 px-4 py-4">
          <Skeleton className="w-full h-12" />
          <Skeleton className="w-full h-12" />
          <Skeleton className="w-full h-12" />
        </div>
      )}
    </section>
  )
}
