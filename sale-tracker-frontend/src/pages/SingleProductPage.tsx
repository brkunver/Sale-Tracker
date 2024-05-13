import SideBar from "@/components/SideBar"
import { useParams } from "react-router-dom"
import { useQuery } from "@tanstack/react-query"
import { getImageUrl, getSingleProduct } from "@/utils/ApiCalls/productApiCalls"

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
        <main className="mx-auto flex-col flex">
          <h1>{query.data.name}</h1>
          <p>{query.data.description}</p>
          <img src={getImageUrl(query.data.imageUrl)} alt={query.data.name} className="rounded lg:max-w-[400px]" />
        </main>
      </div>
    )
  }
}
