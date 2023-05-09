import sys #System
import re #Regex
import getopt as argParser
import pyperclip

def loadText(text):
    with open(text, 'r') as file:
        return file.read()

def validateLink(argument):
    check = r'\[http[s]?:\/\/'
    return bool(re.match(check, argument))

def parseLink(link,joinMainAndLast=False):
    LinkMD = r'\[https?\:\/\/(.*)\]\((.*)\)'
    link = re.match(LinkMD, link) 

    head = link.group(1)
    # print(head)
    # tail = link.group(2)
    # print(tail)

    noSubDirectories = r'(.+?)(?=/)'
    cleanHead = re.match(noSubDirectories, head).group(1)
    # print(cleanHead)
    subDomains = cleanHead.split('.')
    # print(subDomains)

    mainSubdomains = subDomains[-2:]
    # print(mainSubdomains)
    mainDomain = mainSubdomains[0]
    # print(mainDomain)
    # lastDomain = mainSubdomains[1]
    # print(lastDomain)
    if(joinMainAndLast):
        return ".".join(mainSubdomains)
    return mainDomain

# ------------------------------ parse argumens ------------------------------ #
#https://www.tutorialspoint.com/python/python_command_line_arguments.htm
def main(argv):
    try:
        options, arguments = argParser.getopt(argv,"hh:")
    except argParser.GetoptError:
        print('test.py -h <option>')
        sys.exit(2)
    for option, value in options:
        if(option == '-h'):
            if(value != ''):
                print("Ayuda no disponible")
            print(loadText('assets/mainHelp.txt'))
            sys.exit(1)

    # print(arguments)
    argumentCount = len(arguments)
    link = ''

    if(argumentCount == 0):
        #Si no se especifico ningun valor, pedimos uno para trabajar.
        link = input("Please enter a link: ")

    if(argumentCount > 1):
        print("Warning!: Se ha entregado mas de un argumento, se evaluara solo el primero")

    if(link == '' and argumentCount > 0):
        link = arguments[0]

    validLink = validateLink(link)
    # print(f'Is a valid link: {validLink}')
    if(validLink == False):
        print("Link invalido, por favor ingresa un link como la siguiente")
        print("Ej: [http://www.makeuseof.com/service/diy-projects/](http://www.makeuseof.com/service/diy-projects/)")
        sys.exit(2)
    parsedLink = parseLink(link)
    print(f'El resultado: {parsedLink} ha sido copiado al portapapeles!')
    pyperclip.copy(parsedLink)

if __name__ == "__main__":
    main(sys.argv[1:])