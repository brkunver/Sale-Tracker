import SideBar from "@/components/SideBar"
import { useParams } from "react-router-dom"
import { useQuery } from "@tanstack/react-query"
import { getSingleCustomer } from "@/utils/ApiCalls/customerApiCalls"
import EditCustomer from "@/components/customers/EditCustomer"

export default function EditCustomerPage() {
  let { id } = useParams<{ id: string }>()
  const query = useQuery({
    queryKey: ["single-customer", id],
    queryFn: () => getSingleCustomer(id!),
    enabled: !!id,
  })

  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto flex-col flex items-center">
        <h1 className="font-bold text-3xl my-4">Edit Customer</h1>
        {query.isLoading && <div>Loading...</div>}
        {!query.data && <div>No Data</div>}
        {query.isSuccess && (
          <section className="grid lg:grid-cols-2 my-4">
            <EditCustomer customer={query.data} />
          </section>
        )}
      </main>
    </div>
  )
}
