import SideBar from "@/components/SideBar"
import { useParams, redirect } from "react-router-dom"

export default function SingleProductPage() {
  let { id } = useParams<{ id: string }>()
  if (!id) {
    redirect("/products")
  }
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <div>{id}</div>
    </div>
  )
}
