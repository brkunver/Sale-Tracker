import { LoaderCircle } from "lucide-react"
import { cn } from "@/lib/utils"

function LoadingCard({ className }: { className?: string }) {
  return (
    <div className={cn("flex flex-col place-content-center", className)}>
      <h2 className="mx-auto text-center font-bold text-2xl">Loading</h2>
      <LoaderCircle size={48} className="animate-spin mx-auto" />
    </div>
  )
}

export default LoadingCard
