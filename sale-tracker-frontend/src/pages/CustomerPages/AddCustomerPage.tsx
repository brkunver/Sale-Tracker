import SideBar from "@/components/SideBar"

function AddCustomerPage() {
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="flex flex-col lg:flex-row mx-auto items-center">
        <h1 className="text-center font-bold text-2xl mb-4">Add Customer</h1>
      </main>
    </div>
  )
}

export default AddCustomerPage
