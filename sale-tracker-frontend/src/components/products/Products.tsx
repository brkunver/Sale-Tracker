import { getAllProducts, getImageUrl } from "@/utils/productApiCalls"
import { useQuery } from "@tanstack/react-query"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { CircleX, LoaderCircle } from "lucide-react"

interface Props {
  page?: number
  count?: number
}
export default function Products(props: Props) {
  let page = props.page ?? 1
  let count = props.count ?? 5
  let query = useQuery({
    queryKey: ["products", page],
    queryFn: async () => await getAllProducts(page, count),
  })

  if (query.isLoading) {
    return (
      <div className="text-center grid place-content-center lg:min-w-[300px] lg:min-h-[300px]">
        <div className="flex flex-col items-center justify-center">
          <LoaderCircle size={48} className="animate-spin" />
          <p>Loading Products...</p>
        </div>
      </div>
    )
  }
  if (query.isError) {
    return (
      <div className="text-center grid place-content-center lg:min-w-[300px] lg:min-h-[300px] ">
        <div className="flex flex-col items-center justify-center space-y-2">
          <CircleX size={48} />
          <p>Error Loading Products</p>
          <button onClick={() => query.refetch()} className="bg-blue-700 text-gray-50 py-2 px-4 rounded">
            Try Again
          </button>
        </div>
      </div>
    )
  }

  return (
    <Table className="w-fit">
      <TableHeader>
        <TableRow>
          <TableHead className="min-w-16 lg:min-w-24">Image</TableHead>
          <TableHead className="min-w-16 lg:min-w-24 text-center">Name</TableHead>
          <TableHead className="min-w-16 lg:min-w-24 text-right">Price</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {query.data?.data.map((product) => (
          <TableRow key={product.productId}>
            <TableCell>
              <img
                src={getImageUrl(product.imageUrl)}
                alt={product.name}
                className="w-10 h-10 rounded-full object-cover"
              />
            </TableCell>
            <TableCell>{product.name}</TableCell>
            <TableCell className="text-right text-green-800 text-base">{product.price}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
