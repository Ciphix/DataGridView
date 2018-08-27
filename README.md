# DataGridView

## What it does

This custom activity is used to display any kind of data in a Windows Form using a dataGridView. The input is a dataTable and how the screen should be displayed, the output is the new dataTable and the last selected cell.

This could be used to change data, selected cells and transforming unstructured data to structured data with human assistance, by asking the user to select a cell in the column with a certain type of data.

For a small showcase please see: [plotTable showcase](https://vimeo.com/286331402)

**Input**
These are the input parameters
```
InputTable - DataTable - The data to be displayed
Suffix (optional) - String - The name of the window
ShowAsDialog (optional) - Boolean - If the UiPath code should continue running or should wait for the form to be closed
```

**Processing**
It displays a Windows Form containing only a DataGridView element
That's it! A really simple and elegant data display.

**Output**
These are the output parameters
```
OutputTable - DataTable - The altered data
SelectedCell - Dictionary<string, string> - All the information about the last selected cell:
  rowIndex - The row number of the cell
  columnIndex - The index of the last selected column
  columnName - The name of the column of the cell
  value - The value of the cell in string form
```

## How we built it

Built in Visual Studio using C# implementing a CodeActivity


## Credits
**Sebastiaan Smits**

**Bas van der Pol**

Ciphix B.V. @ Rotterdam
