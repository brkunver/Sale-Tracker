import { Customer } from "@/types/customerTypes"


interface Props {
  customer: Customer
}

function EditCustomer(props: Props) {
  return <div>
    {props.customer.name}
  </div>
}

export default EditCustomer
