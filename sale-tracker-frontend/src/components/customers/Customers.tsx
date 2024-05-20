import { useQuery } from "@tanstack/react-query"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { CircleX, FilePenLine, LoaderCircle } from "lucide-react"
import { cn } from "@/lib/utils"
import { Link } from "react-router-dom"
import { getAllCustomers } from "@/utils/ApiCalls/customerApiCalls"
import DeleteCustomer from "./DeleteCustomer"

interface Props {
  page?: number
  count?: number
  className?: string
  showDelete?: boolean
}
export default function Customers(props: Props) {
  let page = props.page ?? 1
  let count = props.count ?? 5
  let query = useQuery({
    queryKey: ["customers", page, count],
    queryFn: async () => await getAllCustomers({ page: page, count: count }),
  })

  if (query.isLoading) {
    return (
      <div className="text-center grid place-content-center lg:min-w-[300px] lg:min-h-[300px]">
        <div className="flex flex-col items-center justify-center">
          <LoaderCircle size={48} className="animate-spin" />
          <p>Loading Customers...</p>
        </div>
      </div>
    )
  }
  if (query.isError) {
    return (
      <div className="text-center grid place-content-center lg:min-w-[300px] lg:min-h-[300px] ">
        <div className="flex flex-col items-center justify-center space-y-2">
          <CircleX size={48} />
          <p className="text-red-500 text-lg">Error Loading Customers</p>
          <button onClick={() => query.refetch()} className="bg-blue-700 text-gray-50 py-2 px-4 rounded">
            Try Again
          </button>
        </div>
      </div>
    )
  }

  return (
    <Table className={cn("w-fit mx-auto", props.className)}>
      <TableHeader>
        <TableRow>
          {props.showDelete && <TableHead className="min-w-10 text-center">Delete</TableHead>}
          <TableHead className="min-w-10 lg:min-w-24 text-center">Name</TableHead>

          {props.showDelete && <TableHead className="min-w-10 text-center">Edit</TableHead>}
        </TableRow>
      </TableHeader>
      <TableBody>
        {query.data?.reverse().map((customer) => (
          <TableRow key={customer.id}>
            {props.showDelete && (
              <TableCell className="min-w-10 text-center">
                <DeleteCustomer />
              </TableCell>
            )}

            <TableCell className="text-center">
              <Link to={`/customer/${customer.id}`}>{customer.name}</Link>
            </TableCell>

            {props.showDelete && (
              <TableCell className="min-w-10 text-center">
                <Link to={`/edit-customer/${customer.id}`}>
                  <FilePenLine className="text-blue-700 hover:scale-110 duration-100" />
                </Link>
              </TableCell>
            )}
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
