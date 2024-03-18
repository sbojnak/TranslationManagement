import { useEffect, useState } from "react";
import { addTranslator, getTranslators } from "../../../apiServices/translatorsService";
import { Translator } from "../../../apiTypes/Translator";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from 'react-hook-form';
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";


const TranslatorsList = () => {
  const [translators, setTranslators] = useState<Translator[] | null>();
  const [isAddTranslateDialogOpen, setIsAddTranslateDialogOpen] = useState(false);

  async function getAllTranslators(){
    const translatorsFromApi = await getTranslators();
    setTranslators(translatorsFromApi);
  }

  useEffect(() => {
    getAllTranslators();
  }, [translators]);

  const addNewTranslator = (translator: Translator) => {
    addTranslator(translator);
    setIsAddTranslateDialogOpen(false);
    getAllTranslators();
  }

  const formSchema = z.object({
    name: z.string().min(1, {
      message: "Translator's name cannot be empty.",
    }),
    hourlyRate: z.string().min(1, {
      message: "Hourly rate has to be at least 0.",
    }),
    status: z.string(),
    creditCardNumber: z.string(),
  })

  const form = useForm<Translator>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      hourlyRate: "0",
      status: "",
      creditCardNumber: "",
    },
  })

  return (
    <>
      <Dialog open={isAddTranslateDialogOpen} onOpenChange={setIsAddTranslateDialogOpen}>
        <DialogTrigger asChild>
          <Button>Add translator</Button>
        </DialogTrigger>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>Add translator</DialogTitle>
            <DialogDescription>
              Make changes to your profile here. Click save when you're done.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <Form {...form}>
              <form onSubmit={form.handleSubmit(addNewTranslator)} className="space-y-8">
                <FormField
                  control={form.control}
                  name="name"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Name</FormLabel>
                      <FormControl>
                        <Input {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="hourlyRate"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Hourly rate</FormLabel>
                      <FormControl>
                        <Input {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="status"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Status</FormLabel>
                      <FormControl>
                        <Input placeholder="shadcn" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="creditCardNumber"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Credit card number</FormLabel>
                      <FormControl>
                        <Input placeholder="shadcn" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <Button type="submit">Submit</Button>
              </form>
            </Form>
          </div>
        </DialogContent>
      </Dialog>

      <table>
        <thead>
          <tr>
            <th className="px-2">Name</th>
            <th className="px-2">Hourly rate</th>
            <th className="px-2">Status</th>
          </tr>
        </thead>
        <tbody>
          {translators?.map(translator => (
            <tr className="py-1" key={translator.id}>
              <td className="px-2">{translator.name}</td>
              <td className="px-2">{translator.hourlyRate}</td>
              <td className="px-2">{translator.status}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}

export default TranslatorsList;