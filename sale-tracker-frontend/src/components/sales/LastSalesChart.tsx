import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js"
import { Line } from "react-chartjs-2"
import { useQuery } from "@tanstack/react-query"
import { getLastSales } from "@/utils/saleApiCalls"
import { cn } from "@/lib/utils"

type Props = {
  day: number
  className?: string
}

export default function LastSalesChart({ day , className }: Props) {
  const lastSalesQuery = useQuery({
    queryKey: ["last-sales", day],
    queryFn: async () => await getLastSales(day ?? 10),
  })

  ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)

  if (lastSalesQuery.isLoading) {
    return <p className="mx-auto text-lg min-w-20 min-h-5 rounded border-4 border-green-600 px-4 py-2">Loading Last Sale Revenue Graph...</p>
  }

  if (lastSalesQuery.isError) {
    return <p className="mx-auto text-lg min-w-20 min-h-5 rounded border-4  border-red-600 px-4 py-2">Error Loading Data</p>
  }
  const chartData = {
    labels: lastSalesQuery.data!.map((_, index) => `${index + 1}`),
    datasets: [
      {
        label: "Revenue",
        data: lastSalesQuery.data!,
        fill: false,
        borderColor: "rgb(75, 192, 192)",
        tension: 0.1,
      },
    ],
  }
  return <Line data={chartData} options={{ maintainAspectRatio: false, responsive : true }} className={cn("lg:w-full lg:h-[24rem]", className)} />
}
