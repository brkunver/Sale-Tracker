export type Customer = {
  id: string
  name: string
  phone: string
  address: string
  createdOn: string
  updatedOn: string
  isDeleted: boolean
}

export type CustomerData = {
  success: boolean
  message: string
  data: Customer[]
}
