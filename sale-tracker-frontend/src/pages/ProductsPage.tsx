import SideBar from "@/components/SideBar"
import Products from "@/components/products/Products"
import { getCount } from "@/utils/productApiCalls"
import { useQuery } from "@tanstack/react-query"
import { LoaderCircle } from "lucide-react"
import { useState } from "react"

import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination"

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
          <p>
            <p>Current Page = {page}</p>
          </p>
        </div>
        <div className="px-2">
          <div className="flex flex-col">
            <Products className="" page={page} count={10} showDelete={true} />
            <Pagination className="mt-2">
              <PaginationContent>
                <PaginationItem>
                  <PaginationPrevious onClick={() => setPage((prev) => prev - 1)} />
                </PaginationItem>
                {page != 1 ? <PaginationItem>
                  <PaginationLink onClick={() => setPage((prev) => prev - 1)}>{page - 1}</PaginationLink>
                </PaginationItem> : null}
                <PaginationItem>
                  <PaginationLink className="bg-green-800 text-white">{page}</PaginationLink>
                </PaginationItem>
                <PaginationItem>
                  <PaginationLink onClick={() => setPage((prev) => prev + 1)} >{page + 1}</PaginationLink>
                </PaginationItem>
                <PaginationItem>
                  <PaginationEllipsis />
                </PaginationItem>
                <PaginationItem>
                  <PaginationNext onClick={() => setPage((prev) => prev + 1)} />
                </PaginationItem>
              </PaginationContent>
            </Pagination>
          </div>
        </div>
      </main>
    </div>
  )
}

export default ProductsPage
