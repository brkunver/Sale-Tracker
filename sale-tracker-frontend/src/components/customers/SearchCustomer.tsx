import { getAllCustomers } from "@/utils/ApiCalls/customerApiCalls"
import { useState, useEffect } from "react"
import { Link } from "react-router-dom"
import { Input } from "../ui/input"
import { Product } from "@/types/productTypes"
import { Skeleton } from "../ui/skeleton"
import { Customer } from "@/types/customerTypes"

function SearchCustomer() {
  const [inputValue, setInputValue] = useState("")
  const [debouncedValue, setDebouncedValue] = useState("")
  const [customers, setCustomers] = useState<Customer[]>()
  const [loading, setLoading] = useState(false)
  const delay = 500 // 500 ms delay

  // Debounce effect
  useEffect(() => {
    setLoading(true)
    setCustomers(undefined)
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
        let data = await getAllCustomers({ name: debouncedValue })
        setLoading(false)
        setCustomers(data)
      }

      fetchProducts()
    }
    setCustomers(undefined)
  }, [debouncedValue])

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInputValue(e.target.value)
  }
  return (
    <section className="flex flex-col">
      <Input onChange={handleChange} value={inputValue} className="lg:min-w-[500px]" />
      <section id="found-products" className="flex flex-col gap-4 my-6">
        {customers &&
          customers.map((customer) => (
            <Link
              to={`/customer/${customer.id}`}
              key={customer.id}
              className="flex items-center gap-2 rounded border hover:outline outline-1 p-2 "
            >
              <p className="font-semibold">{customer.name}</p>
              <p className="font-semibold">
                | Phone <span>{customer.phone}</span>
              </p>
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

export default SearchCustomer
