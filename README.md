# MoCore
A Windows tool that offers a lot of functionalities like sorting files, cleaning old files, searching for files in the whole hard disk, File Encryption and Decryption System, Performance Monitoring.

### Class Relationships and Multiplicity

| Classes                        | Relationship   | Multiplicity                          | Explanation                                                                                  |
|--------------------------------|----------------|---------------------------------------|----------------------------------------------------------------------------------------------|
| Main Window to Dashboard       | Composition    | 1-to-1                                | Main Window contains one Dashboard instance.                                                 |
| Dashboard to ToolBase          | Aggregation    | 1-to-many                             | Dashboard manages and interacts with multiple ToolBase instances.                            |
| ToolBase to Subclasses         | Inheritance    | 1-to-1                                | ToolBase provides common interface, subclasses implement specific functionalities.           |
| FileSorting to SortingStrategy | Dependency     | 1-to-1                                | FileSorting uses a SortingStrategy for sorting operations, allowing different strategies.    |

### 1st Draft below

![image](https://github.com/user-attachments/assets/987e1df8-0fc6-4a9f-b377-cea00d30816d)
