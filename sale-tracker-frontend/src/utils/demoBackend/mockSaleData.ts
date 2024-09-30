import type { Sale, SaleData, SaleDetail, Sales, SingleSaleData } from "@/types/saleTypes"

export const mockSaleData: SaleData = {
  success: true,
  message: "Data retrieved successfully",
  data: [
    {
      id: "sale1",
      saledOn: new Date("2023-01-01"),
      customerId: "cust1",
      total: 1500,
    },
    {
      id: "sale2",
      saledOn: "2023-09-20",
      customerId: "cust2",
      total: 1200,
    },
    {
      id: "sale3",
      saledOn: "2023-09-25",
      customerId: "cust3",
      total: 1800,
    },
    {
      id: "sale4",
      saledOn: "2023-09-30",
      customerId: "cust4",
      total: 1000,
    },
    {
      id: "sale5",
      saledOn: "2023-10-01",
      customerId: "cust5",
      total: 2100,
    },
    {
      id: "sale6",
      saledOn: "2023-10-05",
      customerId: "cust6",
      total: 1300,
    },
    {
      id: "sale7",
      saledOn: "2023-10-10",
      customerId: "cust7",
      total: 1700,
    },
    {
      id: "sale8",
      saledOn: "2023-10-15",
      customerId: "cust8",
      total: 900,
    },
    {
      id: "sale9",
      saledOn: "2023-10-20",
      customerId: "cust9",
      total: 2200,
    },
    {
      id: "sale10",
      saledOn: "2023-10-25",
      customerId: "cust10",
      total: 2500,
    },
    {
      id: "sale11",
      saledOn: "2023-10-30",
      customerId: "cust11",
      total: 1400,
    },
  ],
}
