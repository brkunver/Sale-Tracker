import SideBar from "@/components/SideBar"
import Customers from "@/components/customers/Customers"

export default function CustomersPage() {
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main>
        <h1>Customers</h1>
        <Customers showDelete/>
      </main>
    </div>
  )
}
