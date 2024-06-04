import SideBar from "@/components/SideBar"
import Products from "@/components/products/Products"
import { getCount } from "@/utils/ApiCalls/productApiCalls"
import { useQuery } from "@tanstack/react-query"
import { CirclePlus, LoaderCircle, Search } from "lucide-react"
import { useEffect, useState } from "react"

import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination"
import { Link } from "react-router-dom"

function ProductsPage() {
  const [page, setPage] = useState(1)
  const [totalPage, setTotalPage] = useState(0)
  const query = useQuery({
    queryKey: ["products", "count"],
    queryFn: async () => await getCount(),
  })

  useEffect(() => {
    if (query.isSuccess) {
      setTotalPage(Math.ceil(query.data / 10))
    }
  }, [query.data])

  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="grid grid-cols-1 lg:grid-cols-2 items-center mx-auto gap-2">
        <div id="products-table" className="flex flex-col mx-auto">
          <h1 className="text-3xl lg:py-8 mx-auto">Products</h1>
          <div className="flex gap-2 justify-center items-center text-lg font-semibold">
            <p>Total Product Count = </p>
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
          <p className="font-semibold mx-auto">Total Page = {totalPage}</p>
          <Products className="" page={page} count={10} showDelete={true} />
          <Pagination className="mt-2">
            <PaginationContent>
              <PaginationItem>
                <PaginationPrevious
                  onClick={() => {
                    if (page != 1) {
                      setPage((prev) => prev - 1)
                    }
                  }}
                />
              </PaginationItem>
              {page == 1 ? null : (
                <PaginationItem>
                  <PaginationLink onClick={() => setPage((prev) => prev - 1)}>{page - 1}</PaginationLink>
                </PaginationItem>
              )}
              <PaginationItem>
                <PaginationLink className="bg-green-800 text-white hover:bg-green-800 hover:text-white">
                  {page}
                </PaginationLink>
              </PaginationItem>
              {page != totalPage ? (
                <PaginationItem>
                  <PaginationLink onClick={() => setPage((prev) => prev + 1)}>{page + 1}</PaginationLink>
                </PaginationItem>
              ) : null}
              {page != totalPage ? (
                <PaginationItem>
                  <PaginationEllipsis />
                </PaginationItem>
              ) : null}
              <PaginationItem>
                <PaginationNext
                  onClick={() => {
                    if (page != totalPage) {
                      setPage((prev) => prev + 1)
                    }
                  }}
                />
              </PaginationItem>
            </PaginationContent>
          </Pagination>
        </div>
        <div className="px-8 flex flex-col gap-4">
          <Link
            to={"/add-product"}
            className="bg-green-800 hover:bg-green-900 min-h-[100px] min-w-[150px] rounded-md flex text-white justify-center items-center gap-2"
          >
            <CirclePlus size={40} />
            <p className="text-xl">Add New Product</p>
          </Link>
          <Link
            to={"/search-products"}
            className="bg-violet-800 hover:bg-violet-900 min-h-[100px] min-w-[150px] rounded-md flex text-white justify-center items-center gap-2"
          >
            <Search size={40} />
            <p className="text-xl">Search Products</p>
          </Link>
        </div>
      </main>
    </div>
  )
}

export default ProductsPage
