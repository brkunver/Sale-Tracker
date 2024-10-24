import { Sale, SaleDetail, SingleSaleData } from "@/types/saleTypes"

export default function getRandomSingleSaleData(id: string): SingleSaleData {
  const saleDetailsData: SaleDetail[] = [
    {
      productId: "prod1",
      saleId: id,
      quantity: 1,
      saledPrice: 1000,
    },
  ]

  const saleData: Sale = {
    customerId: "cust1",
    id: id,
    total: 1500,
    saledOn: "2024-05-05",
    saleDetails: saleDetailsData,
  }

  const data: SingleSaleData = {
    success: true,
    message: "success",
    data: saleData,
  }

  return data
}
