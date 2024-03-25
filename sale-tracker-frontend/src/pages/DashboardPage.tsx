import SideBar from "@/components/SideBar"

function DashboardPage() {
  return (
    <div className="flex">
      <SideBar />
      <main className="flex flex-col">
        <h1>Dashboard</h1>
      </main>
    </div>
  )
}

export default DashboardPage
