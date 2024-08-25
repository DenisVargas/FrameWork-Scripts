import PyPDF2
from JsonFile import load_json_file
from IndexElement import DOElement

# Rutas de archivos
json_file = 'inputs/index.json'
input_file = 'inputs/input.pdf'
output_file = 'outputs/output.pdf'

def get_page_size(page):
    mediabox = page.mediaBox
    width = mediabox.upperRight[0]
    height = mediabox.upperRight[1]
    return width, height

def Load_PDF(path):
    try:
        file = open(path, 'rb')
        pdf_reader = PyPDF2.PdfFileReader(file)
        return pdf_reader
    except Exception as e:
        raise RuntimeError(f"Error al cargar el archivo PDF: {e}")

def Save_PDF(pdfFile, path):
    try:
        with open(path, 'wb') as file:
            pdfFile.write(file)
    except Exception as e:
        raise RuntimeError(f"Error al guardar el archivo PDF: {e}")

def main():
    # Cargar los datos del índice desde el archivo JSON y construir la estructura del índice
    index_elements = []
    for json_object in load_json_file(json_file)['index']:
        index_elements.append(DOElement.from_json(json_object))

    # Cargar el archivo PDF de entrada
    pdf_reader = Load_PDF(input_file)

    # Obtener el tamaño de la primera página existente
    first_page_size = get_page_size(pdf_reader.getPage(0))
    print("first page size is:")
    print(first_page_size)

    # Crear un objeto PDFWriter
    pdf_writer = PyPDF2.PdfFileWriter()

    # Copiar las páginas del archivo PDF original al PDF de salida
    for page_num in range(pdf_reader.numPages):
        page = pdf_reader.getPage(page_num)
        pdf_writer.addPage(page)

    # Crear la página de índice
    index_page = pdf_writer.addBlankPage(first_page_size[0], first_page_size[1])

    # Construir el contenido del índice
    index_content = ''
    for element in index_elements:
        index_content += f"{element.item} - {element.page}\n"
        children = element.get_children()
        for child in children:
            index_content += f"    {child.item} - {child.page}\n"

    # Establecer el contenido del índice en la página
    index_page.mergePage(pdf_writer.getPage(0))
    index_page.mergePage(PyPDF2.PdfFileReader(index_content.encode('utf-8')).getPage(0))

    # Guardar el archivo PDF de salida con el índice adjunto
    Save_PDF(pdf_writer, output_file)

    print('Índice insertado exitosamente en el archivo PDF.')

if __name__ == "__main__":
    main()
