1. Class
A class is a reference type stored on the managed heap. It is the fundamental building block of object-oriented programming in C#, supporting full inheritance, polymorphism, and reference equality (meaning two variables are equal only if they point to the exact same object in memory). Classes are mutable by default and are best used for complex business models, entities, and objects that require a distinct identity and reference sharing throughout their lifecycle.

2. Struct
A struct is a value type typically stored on the stack (or inline within containing objects), which helps reduce garbage collection pressure. Unlike classes, structs do not support inheritance from other structs (though they can implement interfaces) and use value-based equality. Structs are ideal for small, lightweight data structures—such as geometric points, coordinates, or measurements—where assignment copies the data rather than the reference.

3. Record
A record is a specialized reference type (or value type if declared as a record struct) introduced to model immutable, data-centric types. Records provide built-in value-based equality, meaning two separate record instances with the same property values are considered equal. They are immutable by default using init-only properties and come with built-in support for formatting output and performing non-destructive mutations using the with expression. Records are best used for Data Transfer Objects (DTOs) and immutable data models.


2. What is an Indexer?

An indexer is a member that allows instances of a class or struct to be indexed just like arrays.

     You define an indexer using the this keyword along with parameters inside square brackets. It lets you retrieve or set values using array-notation ([]) directly on an object instance.
