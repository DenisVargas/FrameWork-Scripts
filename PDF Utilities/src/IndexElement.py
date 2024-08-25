class DOElement:
    def __init__(self, item, page):
        self.item = item
        self.page = page
        self.children = []

    def add_child(self, child_element):
        self.children.append(child_element)

    def get_child(self, index):
        if index >= 0 and index < len(self.children):
            return self.children[index]
        else:
            return None

    def remove_child(self, index):
        if index >= 0 and index < len(self.children):
            del self.children[index]

    def get_children(self):
        return self.children

    def add_children(self, children_list):
        self.children.extend(children_list)

    def from_json(data):
        element = DOElement(data['item'], data['page'])
        if 'children' in data:
            for child_data in data['children']:
                child_element = DOElement.from_json(child_data)
                element.add_child(child_element)
        return element

# usage example
# from index_element import IndexElement

# # Crear un objeto IndexElement
# element1 = IndexElement("Title1", 4)

# # Agregar hijos
# element1.add_child(IndexElement("Subtitle1", 6))
# element1.add_child(IndexElement("Subtitle2", 8))

# # Obtener un hijo específico
# child = element1.get_child(0)
# print(child.item)  # Imprime "Subtitle1"

# # Eliminar un hijo
# element1.remove_child(1)

# # Obtener todos los hijos
# children = element1.get_children()
# for child in children:
#     print(child.item)

# # Agregar una lista de hijos
# children_list = [
#     IndexElement("Subtitle3", 10),
#     IndexElement("Subtitle4", 12)
# ]
# element1.add_children(children_list)

