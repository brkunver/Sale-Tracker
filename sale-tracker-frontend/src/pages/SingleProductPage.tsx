import SideBar from "@/components/SideBar"
import { useParams } from "react-router-dom"
import { useQuery } from "@tanstack/react-query"
import { getSingleProduct } from "@/utils/ApiCalls/productApiCalls"

export default function SingleProductPage() {
  let { id } = useParams<{ id: string }>()
  const query = useQuery({
    queryKey: ["single-product", id],
    queryFn: () => getSingleProduct(id!),
    enabled: !!id,
  })

  if (query.isLoading) return <div>Loading...</div>
  if (query.isError) return <div>Error: </div>
  if (!query.data) return <div>No Data</div>
  if (query.isSuccess) {
    return (
      <div className="flex min-h-screen">
        <SideBar />
        <div>{query.data!.name}</div>
      </div>
    )
  }
}
