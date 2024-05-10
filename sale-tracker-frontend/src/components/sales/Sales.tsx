import { getAllSales } from "@/utils/saleApiCalls"
import { useQuery } from "@tanstack/react-query"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { CircleX, LoaderCircle } from "lucide-react"
import formatDate from "@/utils/formatDate"
import { cn } from "@/lib/utils"
import { useEffect } from "react"

interface Props {
  page?: number
  count?: number
  className?: string
  setRefetch: React.Dispatch<
    React.SetStateAction<{
      reloadProducts: () => null
      reloadSales: () => null
    }>
  >
}

export default function Sales(props: Props) {
  let page = props.page ?? 1
  let count = props.count ?? 5
  let query = useQuery({
    queryKey: ["sales", page, count],
    queryFn: async () => await getAllSales(page, count),
  })

  useEffect(() => {
    if (props.setRefetch) {
      props.setRefetch((prev) => ({
        ...prev,
        reloadSales: () => {
          query.refetch()
          return null
        },
      }))
    }
  }, [])

  if (query.isLoading) {
    return (
      <div className="text-center grid place-content-center lg:min-w-[300px] lg:min-h-[300px]">
        <div className="flex flex-col items-center justify-center">
          <LoaderCircle size={48} className="animate-spin" />
          <p>Loading Sales...</p>
        </div>
      </div>
    )
  }
  if (query.isError) {
    return (
      <div className="text-center grid place-content-center lg:min-w-[300px] lg:min-h-[300px]">
        <div className="flex flex-col items-center justify-center space-y-2">
          <CircleX size={48} />
          <p className="text-red-500 text-lg">Error Loading Sales</p>
          <button onClick={() => query.refetch()} className="bg-blue-700 text-gray-50 py-2 px-4 rounded">
            Try Again
          </button>
        </div>
      </div>
    )
  }

  return (
    <Table className={cn("w-fit mx-auto",props.className)}>
      <TableHeader>
        <TableRow>
          <TableHead className="min-w-10 lg:min-w-24 text-center">Saled on</TableHead>
          <TableHead className="min-w-10 lg:min-w-24 text-center">Total</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {query.data?.data.map((sale) => (
          <TableRow key={sale.id}>
            <TableCell className="text-center ">{formatDate(sale.saledOn)}</TableCell>
            <TableCell className="text-center text-green-800 text-base">{sale.total}$</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
