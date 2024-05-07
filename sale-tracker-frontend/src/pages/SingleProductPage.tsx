import SideBar from "@/components/SideBar"
import { useParams } from "react-router-dom"

export default function SingleProductPage() {
  let { id } = useParams<{ id: string }>()
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <div>{id}</div>
    </div>
  )
}
