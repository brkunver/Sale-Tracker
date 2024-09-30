import { Sale, SaleData, SingleSaleData } from "@/types/saleTypes"

export default function getRandomSingleSaleData(id: string): SingleSaleData {
  const saleDetailsData  = [
      {
        
      }
  ]

  const saleData: Sale = {
    customerId: "cust1",
    id: id,
    total : 1500,
    saledOn : "2024-05-05",
    saleDetails : saleDetailsData
  }

  const data: SingleSaleData = {
    success: true,
    message: "success",
    data: saleData,
  }

  return data
}
