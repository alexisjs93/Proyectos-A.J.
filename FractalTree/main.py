## 12.10.2021
## Tutorial

import turtle


## SE DEFINEN LAS VARIABLES Y LA CONDICIÓN FINAL
MINIMUM_BRANCH_LENGTH = .03
BranchLength = 40
ShortenBy = 4.5
Angle = 20

## FUNCIÓN RECURSIVA, TERMINA AL LLEGAR A LA LONGITUD MÍNIMA
def build_tree(t, branch_length, shorten_by, angle):
  if branch_length > MINIMUM_BRANCH_LENGTH:
    t.forward(branch_length)
    new_length = branch_length - shorten_by

    t.left(angle)
    build_tree(t, new_length, shorten_by, angle)

    t.right(angle * 2)
    build_tree(t, new_length, shorten_by, angle)

    t.left(angle)
    t.backward(branch_length)

## INICIALIZAR TURTLE
tree = turtle.Turtle()
turtle.tracer(0, 0)
tree.hideturtle()
tree.speed(0)
tree.setheading(90)
tree.color('green')

## LLAMAR LA FUNCIÓN RECURSIVA
build_tree(tree, BranchLength, ShortenBy, Angle)

## GUARDAR COMO ARCHIVO POSTSCRIPT
turtle.getscreen().getcanvas().postscript(file=str(BranchLength)+'_'+str(ShortenBy)+'_'+str(Angle)+'.ps')

turtle.mainloop()
turtle.update()

