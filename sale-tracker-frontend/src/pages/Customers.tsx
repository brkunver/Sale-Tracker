import SideBar from "@/components/SideBar"

export default function CustomersPage() {
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto flex-col flex">
        <h1>Customers</h1>
      </main>
    </div>
  )
}
