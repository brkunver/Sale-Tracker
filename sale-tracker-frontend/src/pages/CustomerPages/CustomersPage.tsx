import SideBar from "@/components/SideBar"
import Customers from "@/components/customers/Customers"
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationPrevious,
  PaginationLink,
  PaginationNext,
  PaginationEllipsis,
} from "@/components/ui/pagination"
import { useQuery } from "@tanstack/react-query"
import { useEffect, useState } from "react"
import { getCustomerCount } from "@/utils/ApiCalls/customerApiCalls"
import { CirclePlus, LoaderCircle, Search } from "lucide-react"
import { Link } from "react-router-dom"

export default function CustomersPage() {
  const [page, setPage] = useState(1)
  const [totalPage, setTotalPage] = useState(0)
  const query = useQuery({
    queryKey: ["customers", "count"],
    queryFn: async () => await getCustomerCount(),
  })

  useEffect(() => {
    if (query.isSuccess) {
      setTotalPage(Math.ceil(query.data / 10))
    }
  }, [query.data])

  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="flex flex-col lg:flex-row mx-auto items-center">
        <section id="customer-table">
          <h1 className="text-center font-bold text-2xl mb-4">Customers</h1>
          <p className="font-semibold text-center">Total Page Count : {totalPage}</p>
          <div className="flex gap-2 justify-center items-center text-lg font-semibold">
            <p>Total Customer Count = </p>
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
          <Customers showDelete page={page} count={10} />
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
        </section>
        <section className="px-8 flex flex-col gap-4">
          <Link
            to={"/add-customer"}
            className="bg-green-800 hover:bg-green-900 min-h-[100px] min-w-[150px] rounded-md flex text-white justify-center items-center gap-2 px-10"
          >
            <CirclePlus size={40} />
            <p className="text-xl">Add New Customer</p>
          </Link>
          <Link
            to={"/search-customers"}
            className="bg-violet-800 hover:bg-violet-900 min-h-[100px] min-w-[150px] rounded-md flex text-white justify-center items-center gap-2 px-10"
          >
            <Search size={40} />
            <p className="text-xl">Search Customers</p>
          </Link>
        </section>
      </main>
    </div>
  )
}
