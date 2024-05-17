import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { Trash2 } from "lucide-react"
import { deleteProduct } from "@/utils/ApiCalls/productApiCalls"
import { Button } from "../ui/button"

interface Props {
  productId: string
  productName: string
  message?: string
}

export default function DeleteProduct(props: Props) {
  const queryClient = useQueryClient()
  const mutation = useMutation({
    mutationFn: async () => {
      return await deleteProduct(props.productId)
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] })
    },
  })
  return (
    <AlertDialog>
      <AlertDialogTrigger asChild>
        {props.message ? (
          <Button className="bg-red-700 hover:bg-red-900 w-fit text-white">
            {props.message} <Trash2 className="text-white" />
          </Button>
        ) : (
          <Trash2 className="text-red-600 hover:scale-110 duration-100 cursor-pointer" role="button" />
        )}
      </AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
          <AlertDialogDescription>
            This action cannot be undone. This will delete : <span className="font-semibold">{props.productName}</span>.
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Cancel</AlertDialogCancel>
          <AlertDialogAction onClick={() => mutation.mutate()} className="bg-red-700 hover:bg-red-900">
            Delete
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
