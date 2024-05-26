import SideBar from "@/components/SideBar"
import { Link, useParams } from "react-router-dom"
import { useQuery } from "@tanstack/react-query"
import { Button } from "@/components/ui/button"
import { getSingleCustomer } from "@/utils/ApiCalls/customerApiCalls"
export default function SinglecustomerPage() {
  let { id } = useParams<{ id: string }>()
  const query = useQuery({
    queryKey: ["single-customer", id],
    queryFn: () => getSingleCustomer(id!),
    enabled: !!id,
  })

  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto flex flex-col lg:flex-row min-h-screen">
        {query.isLoading && <div>Loading...</div>}
        {query.isError && <div>Error</div>}
        {query.isSuccess && (
          <section id="customer-section" className="grid lg:grid-cols-2 mt-8 lg:gap-4 justify-start items-start h-fit">
            <section id="customer-info" className="flex flex-col h-full gap-2">
              <h1 className="text-xl font-bold">{query.data.name}</h1>
              <p className="italic">{query.data.phone}</p>
              <p className="font-bold">
                Adress : <span className="font-extrabold text-blue-700">{query.data.address} </span>
              </p>
              <Button className="bg-blue-600 hover:bg-blue-800 w-fit text-white">
                <Link to={`/edit-customer/${query.data.id}`}>Edit Customer</Link>
              </Button>
            </section>
          </section>
        )}
      </main>
    </div>
  )
}
