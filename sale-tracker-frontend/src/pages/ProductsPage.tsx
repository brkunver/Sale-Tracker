import SideBar from "@/components/SideBar"
import Products from "@/components/products/Products"
import { getCount } from "@/utils/productApiCalls"
import { useQuery } from "@tanstack/react-query"
import { LoaderCircle } from "lucide-react"

function ProductsPage() {
  let query = useQuery({
    queryKey: ["productCount"],
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
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-10 px-2 ">
          <Products className="" page={1} count={10} />
        </div>
      </main>
    </div>
  )
}

export default ProductsPage
