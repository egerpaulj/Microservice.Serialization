# Microservice.Serialization 

.NET library that provides a common interface for Serialization.

**JsonConverter** allows defining how to Serialize/Deserialize objects. Complex composite types, with abstract classes can be Serialized/Deserialized by implementing the abstract **JsonConverter**

```
public interface IJsonConverterProvider
{
    JsonConverter[] GetJsonConverters();
}
```

Additionally, for plain objects an empty provider is available for dependency injection.

```
public class EmptyJsonConverterProvider : IJsonConverterProvider
{
    JsonConverter[] GetJsonConverters() => null;
}
```

## Next steps
- Move all serialization logic to a generic library
- Provide an example implementation of JsonConverter

## License

Copyright (C) 2021  Paul Eger

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
