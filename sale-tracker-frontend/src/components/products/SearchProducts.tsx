import { getAllProducts } from "@/utils/ApiCalls/productApiCalls"
import { useState, useEffect } from "react"
import { Link } from "react-router-dom"
import { Input } from "../ui/input"
import { Product } from "@/types/productTypes"

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
    <section>
      <Input onChange={handleChange} value={inputValue} />
      {products && products.map((item) => <p key={item.id}>{item.name}</p>)}
      {loading && <p>Loading...</p>}
    </section>
  )
}
