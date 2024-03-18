import { useEffect, useState } from "react";
import { getTranslators } from "../../../apiServices/translatorsService";
import { Translator } from "../../../apiTypes/Translator";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import AddTranslatorForm from "../AddTranslatorForm/AddTranslatorForm";


const TranslatorsList = () => {
  const [translators, setTranslators] = useState<Translator[] | null>();
  const [isAddTranslateDialogOpen, setIsAddTranslateDialogOpen] = useState<boolean>(false);

  async function getAllTranslators(){
    console.log("getAllTranslators");
    const translatorsFromApi = await getTranslators();
    setTranslators(translatorsFromApi);
  }

  const onNewTranslatorAdded = () => {
    console.log("onNewTranslatorAdded");
    setIsAddTranslateDialogOpen(false);
    getAllTranslators();
  }

  const onOpenChange = (isOpen: boolean) => {
    setIsAddTranslateDialogOpen(isOpen);
    getAllTranslators();
  }

  useEffect(() => {
    console.log("useEffect");
    getAllTranslators();
  }, [isAddTranslateDialogOpen]);

  return (
    <>
      <Dialog open={isAddTranslateDialogOpen} onOpenChange={onOpenChange}>
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

          <AddTranslatorForm onNewTranslatorAdded={onNewTranslatorAdded} />
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