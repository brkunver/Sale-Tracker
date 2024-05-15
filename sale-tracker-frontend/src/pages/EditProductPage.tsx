import SideBar from "@/components/SideBar"
import { useParams } from "react-router-dom"
import { useQuery } from "@tanstack/react-query"
import { getImageUrl, getSingleProduct } from "@/utils/ApiCalls/productApiCalls"
import EditProduct from "@/components/products/EditProduct"

export default function EditProductPage() {
  let { id } = useParams<{ id: string }>()
  const query = useQuery({
    queryKey: ["single-product", id],
    queryFn: () => getSingleProduct(id!),
    enabled: !!id,
  })

  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto flex-col flex items-center">
        <h1 className="font-bold text-3xl my-4">Edit Product</h1>
        {query.isLoading && <div>Loading...</div>}
        {!query.data && <div>No Data</div>}
        {query.isSuccess && (
          <section className="grid lg:grid-cols-2 my-4">
            <section id="product-info">
              <h2 className="font-semibold text-lg text-center">{query.data.name}</h2>
              <img
                src={getImageUrl(query.data.imageUrl)}
                alt={query.data.name}
                className="w-40 rounded object-cover mx-auto"
              />
            </section>
            <EditProduct product={query.data} />
          </section>
        )}
      </main>
    </div>
  )
}
