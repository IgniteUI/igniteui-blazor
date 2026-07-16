// using System;
// using System.Collections.Generic;
// using System.Collections;
// using System.Collections.Specialized;
// using System.Runtime.InteropServices;

// namespace Infragistics.Blazor.Controls 
// {
//     internal class UnmarshalledDataSourceItem
//         : IJSDataSourceItem
//     {
//         private Guid _id;
//         private bool _isNull = false;
        
//         public bool IsNull 
//         {
//             get
//             {
//                 return _isNull;
//             }
//         }

//         public UnmarshalledDataSourceItem() {
//             _id = Guid.NewGuid();
//         }

//         public Guid Id 
//         {
//             get
//             {
//                 return _id;
//             }
//         }

//         public static JSDataSourceSchema ExtractSchema(object item) {
//             if (item == null) {
//                 return  null;
//             }

//             Type c = item.GetType();
//             if (c.IsArray) {
//                 JSDataSourceSchema s = new JSDataSourceSchema();
//                 s.IsDataSource = true;
//                 s.Commit();
//                 return s;
//             }
//             else if (item is IList) {
//                 JSDataSourceSchema s = new JSDataSourceSchema();
//                 s.IsDataSource = true;
//                 s.Commit();
//                 return s;
//             }
//             else if (item is IEnumerable) {
//                 JSDataSourceSchema s = new JSDataSourceSchema();
//                 s.IsDataSource = true;
//                 s.Commit();
//                 return s;
//             }

//             return JSDataSourceSchema.Create(c);
//         }
//     }

// }