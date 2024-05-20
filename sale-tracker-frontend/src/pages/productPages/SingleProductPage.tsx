import SideBar from "@/components/SideBar"
import { Link, useParams } from "react-router-dom"
import { useQuery } from "@tanstack/react-query"
import { getImageUrl, getSingleProduct } from "@/utils/ApiCalls/productApiCalls"
import { Button } from "@/components/ui/button"
import DeleteProduct from "@/components/products/DeleteProduct"

export default function SingleProductPage() {
  let { id } = useParams<{ id: string }>()
  const query = useQuery({
    queryKey: ["single-product", id],
    queryFn: () => getSingleProduct(id!),
    enabled: !!id,
  })

  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto flex flex-col lg:flex-row min-h-screen">
        {query.isLoading && <div>Loading...</div>}
        {query.isError && <div>Error</div>}
        {query.isSuccess && (
          <section id="product-section" className="grid lg:grid-cols-2 mt-8 lg:gap-4 justify-start items-start h-fit">
            <img
              src={getImageUrl(query.data.imageUrl)}
              alt={query.data.name}
              className="rounded w-96 object-contain border p-4"
            />
            <section id="product-info" className="flex flex-col h-full gap-2">
              <h1 className="text-xl font-bold">{query.data.name}</h1>
              <p className="italic">{query.data.description}</p>
              <p className="font-bold">
                Price : <span className="font-extrabold text-green-700">{query.data.price} $</span>
              </p>
              <Button className="bg-blue-600 hover:bg-blue-800 w-fit text-white">
                <Link to={`/edit-product/${query.data.id}`}>Edit Product</Link>
              </Button>
              <DeleteProduct productId={query.data.id} productName={query.data.name} message="Delete" />
            </section>
          </section>
        )}
      </main>
    </div>
  )
}
