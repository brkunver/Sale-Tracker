import SideBar from "@/components/SideBar"
import Products from "@/components/products/Products"
import { getCount } from "@/utils/productApiCalls"
import { useQuery } from "@tanstack/react-query"
import { LoaderCircle } from "lucide-react"
import { useState } from "react"

function ProductsPage() {
  const [page, setPage] = useState(1)
  const query = useQuery({
    queryKey: ["products", "count"],
    queryFn: async () => await getCount(),
  })

  return (
    <div className="flex">
      <SideBar />
      <main className="flex flex-col items-center mx-auto">
        <h1 className="text-3xl lg:py-8">Products</h1>
        <div className="flex gap-2 justify-center items-center text-lg font-semibold">
          <p>Total Count = </p>
          <p>
            {query.isError ? (
              <p className="text-red-500">Error</p>
            ) : query.isSuccess ? (
              query.data
            ) : (
              <LoaderCircle size={20} className="animate-spin" />
            )}
          </p>
        </div>
        <div className="px-2">
          <Products className="" page={page} count={10} showDelete={true} />
        </div>

      </main>
    </div>
  )
}

export default ProductsPage
