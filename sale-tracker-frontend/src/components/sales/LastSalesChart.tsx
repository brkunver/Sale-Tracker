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

type Props = {
  day: number
}

export default function LastSalesChart({ day }: Props) {
  const lastSalesQuery = useQuery({
    queryKey: ["last-sales", day],
    queryFn: async () => await getLastSales(day ?? 10),
  })

  ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)

  if (lastSalesQuery.isLoading) {
    return <p>Loading...</p>
  }

  if (lastSalesQuery.isError) {
    return <p>Error Loading Data</p>
  }
  const chartData = {
    labels: lastSalesQuery.data!.map((_, index) => `${index + 1}`),
    datasets: [
      {
        label: "Data",
        data: lastSalesQuery.data!,
        fill: false,
        borderColor: "rgb(75, 192, 192)",
        tension: 0.1,
      },
    ],
  }
  return <Line data={chartData} className="w-full h-96" />
}
