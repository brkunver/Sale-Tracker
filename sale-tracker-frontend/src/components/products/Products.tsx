import { getAllProducts, getImageUrl } from "@/utils/productApiCalls"
import { useQuery } from "@tanstack/react-query"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { CircleX, LoaderCircle } from "lucide-react"
import { cn } from "@/lib/utils"
import DeleteProduct from "./DeleteProduct"
import { useEffect } from "react"
import { Link } from "react-router-dom"

interface Props {
  page?: number
  count?: number
  className?: string
  showDelete?: boolean
  setRefetch?: React.Dispatch<
    React.SetStateAction<{
      reloadProducts: () => null
      reloadSales: () => null
    }>
  >
}
export default function Products(props: Props) {
  let page = props.page ?? 1
  let count = props.count ?? 5
  let query = useQuery({
    queryKey: ["products", page, count],
    queryFn: async () => await getAllProducts(page, count),
  })

  useEffect(() => {
    if (props.setRefetch) {
      props.setRefetch((prev) => ({
        ...prev,
        reloadProducts: () => {
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
          <p className="text-red-500 text-lg">Error Loading Products</p>
          <button onClick={() => query.refetch()} className="bg-blue-700 text-gray-50 py-2 px-4 rounded">
            Try Again
          </button>
        </div>
      </div>
    )
  }

  return (
    <Table className={cn("w-fit", props.className)}>
      <TableHeader>
        <TableRow>
          {props.showDelete && <TableHead className="min-w-10 text-center">Delete</TableHead>}
          <TableHead className="min-w-10 lg:min-w-24">Image</TableHead>
          <TableHead className="min-w-10 lg:min-w-24 text-center">Name</TableHead>
          <TableHead className="min-w-10 lg:min-w-24 text-right">Price</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {query.data?.data.map((product) => (
          <TableRow key={product.id}>
            {props.showDelete && (
              <TableCell className="min-w-10 text-center">
                <DeleteProduct productId={product.id} />
              </TableCell>
            )}
            <TableCell>
              <Link to={`/product/${product.id}`}>
                <img
                  src={getImageUrl(product.imageUrl)}
                  alt={product.name}
                  className="w-10 h-10 rounded-full object-cover"
                />
              </Link>
            </TableCell>
            <TableCell className="text-center">
              <Link to={`/product/${product.id}`}>{product.name}</Link>
            </TableCell>
            <TableCell className="text-right text-green-800 text-base">{product.price}$</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
