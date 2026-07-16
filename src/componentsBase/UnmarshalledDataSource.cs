using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace IgniteUI.Blazor.Controls
{
    internal class UnmarshalledColumnData
    {
        public UnmarshalledColumnData()
        {
            IntValues = null;
            LongValues = null;
            DoubleValues = null;
            StringValues = null;
            NullValues = null;

            SubColumns = null;
            SubSchema = null;
        }

        public string PropertyPath { get; set;}
        public JSDataSourceSchemaType Type { get; set; }
        public int[] IntValues { get; set; }
        public long[] LongValues { get; set; }
        public double[] DoubleValues { get; set; }
        public string[] StringValues { get; set; }
        public bool[] NullValues { get; set; }
        public Guid[] IDValues { get; set; }
        public UnmarshalledColumn[][] SubDataSourceValues;

        public UnmarshalledColumn Column { get; set; }

        public bool IsObjectColumn { get; set; }

        public UnmarshalledColumnData[] SubColumns { get; set; }
        public JSDataSourceSchema SubSchema { get; set; }
        public Action<int, UnmarshalledColumnData, int, object> Insert { get; internal set; }
        public Action<int, UnmarshalledColumnData, int, object, object> Update { get; internal set; }
        public Action<int, UnmarshalledColumnData, int> Remove { get; internal set; }
        public bool IsIDColumn { get; internal set; }
        public Action<int, UnmarshalledColumnData> Clear { get; internal set; }
        public string PropertyName { get; internal set; }
        public bool IsSubDataSource { get; internal set; }
        public Delegate Getter { get; internal set; }
    }

    [StructLayout(LayoutKind.Explicit, Size=56)]
    internal struct UnmarshalledColumn
    {
        [FieldOffset(0)]
        public int ActualCount;
        [FieldOffset(8)]
        public string DataSourceID;
        [FieldOffset(16)]
        public JSDataSourceSchemaType Type;
        [FieldOffset(24)]
        public string PropertyPath;
        [FieldOffset(32)]
        public int IsSubDataSource;
        [FieldOffset(40)]
        public double[] DoubleValues;
        [FieldOffset(40)]
        public int[] IntValues;
        [FieldOffset(40)]
        public long[] LongValues;
        [FieldOffset(40)]
        public string[] StringValues;
        [FieldOffset(40)]
        public UnmarshalledColumn[][] SubDataSourceValues;
        [FieldOffset(48)]
        public bool[] NullValues;
    }

    internal class UnmarshalledDataSource
        : IJSDataSource 
        {
            public bool SuppressModifications { get; set; }
             public JSDataSourceType DataSourceType
            {
                get
                {
                    return JSDataSourceType.Unmarshalled;
                }
            }
            public bool IsSent { get; set; }
        private object _originalData;

        private Dictionary<Guid, object> _uuidToOriginal = new Dictionary<Guid, object>();
        private Dictionary<object, Guid> _originalToUuid = new Dictionary<object, Guid>();
        //private Dictionary<string, Guid> _stringToUuid = new Dictionary<string, Guid>();
        private RuntimeHelper _helper;
        private JSDataSourceSchema _parentSchema = null;
        private string _parentId;
        private DataSourceManager _manager = null;

        private UnmarshalledColumnData[] _columns = null;

        private Dictionary<Guid, Dictionary<string, UnmarshalledDataSource>> _subDataSources = new Dictionary<Guid, Dictionary<string, UnmarshalledDataSource>>();

        private Func<object, Guid> _idGetter;

        private int _size = 0;
        private int _capacity = 0;
        internal int Capacity 
        {
            get
            {
                return _capacity;
            }
            set
            {
                var oldValue = _capacity;
                if (oldValue != value)
                {
                    _capacity = value;
                    _columns = AdjustCapacity("", _columns, _schema, oldValue, value);
                }
            }
        }

        public UnmarshalledDataSource()
        {
            _idGetter = (o) => {
                if (o == null)
                {
                    return Guid.Empty;
                }
                if (_originalToUuid.ContainsKey(o))
                {
                    return _originalToUuid[o];
                }
                else
                {
                    var id = Guid.NewGuid();
                    //_stringToUuid.Add(idStr, id);
                    _originalToUuid.Add(o, id);
                    _uuidToOriginal.Add(id, o);
                    return id;
                }
            };
        }

        private UnmarshalledColumnData[] AdjustCapacity(string parentPath, UnmarshalledColumnData[] columns, JSDataSourceSchema schema, int oldValue, int newValue)
        {
            //Console.WriteLine("adjusting capacity, oldValue: " + oldValue + ", newValue: " + newValue);
            DateTime start = DateTime.Now;
            if (columns == null && schema == null)
            {
                return null;
            }

            if (schema.IsDataSource)
            {
                if (columns == null)
                {
                    columns = new UnmarshalledColumnData[2];
                    columns[0] = CreateColumn(parentPath, "___self", schema, JSDataSourceSchemaType.ObjectValue, 
                    (Func<object, object>)((o) => o), (o) => o, false);
                    //columns[0].IsSubDataSource = true;
                }
                columns[0] = AdjustColumnCapacity(parentPath, columns[0], schema, "___self", (Func<object, object>)((o) => o), (o) => o, false, JSDataSourceSchemaType.ObjectValue, oldValue, newValue);
            }
            else
            {
                if (columns == null)
                {
                    var extraCols = 1;
                    if (columns != _columns)
                    {
                        extraCols = 0;
                    }
                    columns = new UnmarshalledColumnData[schema.PropertyNames.Length + schema.FieldNames.Length + extraCols]; 
                    for (var k = 0; k < columns.Length; k++)
                    {
                        columns[k] = null;
                    }
                }
                int i = 0;

                for (i = 0; i < schema.PropertyNames.Length; i++)
                {
                    columns[i] = AdjustColumnCapacity(parentPath, columns[i], schema, schema.PropertyNames[i], schema.TypedPropertyGetters[i], schema.PropertyGetters[i], false, schema.PropertyTypes[i], oldValue, newValue);
                }
                for (int j = 0; j < _schema.FieldNames.Length; i++, j++)
                {
                    columns[i] = AdjustColumnCapacity(parentPath, columns[i], schema, schema.FieldNames[j], schema.TypedFieldGetters[j], schema.FieldGetters[j], false, schema.FieldTypes[j], oldValue, newValue);
                }
            }
            if (schema.IsPrimitive)
            {
                columns[columns.Length - 1] = AdjustColumnCapacity(parentPath, columns[columns.Length - 1], schema, "___primitiveValueCollection", null, null, false, schema.PrimitiveType, oldValue, newValue);
                return columns;
            }
            if (String.IsNullOrEmpty(parentPath))
            {
                Func<object, Guid> idGetter = (o) => {
                    if (o == null)
                    {
                        return Guid.Empty;
                    }
                    if (_originalToUuid.ContainsKey(o))
                    {
                        return _originalToUuid[o];
                    }
                    else
                    {
                        var id = Guid.NewGuid();
                        //_stringToUuid.Add(idStr, id);
                        _originalToUuid.Add(o, id);
                        _uuidToOriginal.Add(id, o);
                        return id;
                    }
                };
                Func<object, object> untypedIdGetter = (o) => {
                    return idGetter(o);
                };
                if (columns[columns.Length - 1] == null)
                {
                    columns[columns.Length - 1] = CreateColumn(parentPath, "___id", schema, JSDataSourceSchemaType.StringValue, idGetter, untypedIdGetter, true);
                    columns[columns.Length - 1].IsIDColumn = true;
                }
                columns[columns.Length - 1] = AdjustColumnCapacity(parentPath, columns[columns.Length - 1], schema, "___id", idGetter, untypedIdGetter, true, JSDataSourceSchemaType.StringValue, oldValue, newValue);
            }

            //Console.WriteLine("end adjusting capacity: " + (DateTime.Now - start).TotalMilliseconds);
            return columns;
        }

        private UnmarshalledColumnData CreateColumn(string parentPath, string propertyName, JSDataSourceSchema schema, JSDataSourceSchemaType type, Delegate valueGetter, Func<object, object> untypedGetter, bool isIDColumn)
        {
            if (parentPath != null && parentPath.Length > 0)
            {
                parentPath += ".";
            }
             var newColumn = new UnmarshalledColumnData();
             newColumn.Getter = valueGetter;

                
                newColumn.PropertyName = propertyName;
                newColumn.PropertyPath = parentPath + propertyName;
                newColumn.Type = type;
                if (newColumn.Type == JSDataSourceSchemaType.ObjectValue)
                {
                    var subSchema = schema.GetSubSchema(propertyName);
                    if (subSchema == null)
                    {
                        //Console.WriteLine("create column subschema is null: " + propertyName);
                    } else {
                        newColumn.IsSubDataSource = subSchema.IsDataSource;
                        newColumn.SubSchema = subSchema;
                    }
                }
                
                
                var col = new UnmarshalledColumn();
                col.Type = type;
                col.PropertyPath = parentPath + propertyName;
                col.IsSubDataSource = newColumn.IsSubDataSource ? 1 : 0;
                newColumn.Column = col;

                Func<object, Guid> idGetter = null;
                Func<object, double> doubleGetter = null;
                Func<object, float> singleGetter = null;
                Func<object, bool> boolGetter = null;
                Func<object, byte> byteGetter = null;
                Func<object, decimal> decimalGetter = null;
                Func<object, short> shortGetter = null;
                Func<object, long> longGetter = null;
                Func<object, string> stringGetter = null;
                Func<object, DateTime> dateTimeGetter = null;
                Func<object, object> objectGetter = null;

                Func<object, double> floatingPointGetter = null;
                Func<object, int> integerGetter = null;

                Func<object, short?> nullableShortGetter = null;
                Func<object, int?> nullableIntegerGetter = null;
                Func<object, long?> nullableLongGetter = null;
                Func<object, float?> nullableSingleGetter = null;
                Func<object, double?> nullableDoubleGetter = null;
                Func<object, decimal?> nullableDecimalGetter = null;
                Func<object, bool?> nullableBoolGetter = null;
                Func<object, byte?> nullableByteGetter = null;
                Func<object, DateTime?> nullableDateTimeGetter = null;
                Func<object, double?> nullableFloatingPointGetter = null;
                

                switch (newColumn.Type)
                {
                    case JSDataSourceSchemaType.DoubleValue:
                        doubleGetter = (Func<object, double>)valueGetter;
                        floatingPointGetter = doubleGetter; 
                        break;
                    case JSDataSourceSchemaType.SingleValue:
                        singleGetter = (Func<object, float>)valueGetter;
                        floatingPointGetter = (o) => (double)singleGetter(o);
                        break;
                    case JSDataSourceSchemaType.BooleanValue:
                        boolGetter = (Func<object,bool>)valueGetter;
                        integerGetter = (o) => boolGetter(o) ? 1 : 0;
                        break;
                    case JSDataSourceSchemaType.ByteValue:
                        byteGetter = (Func<object,byte>)valueGetter;
                        integerGetter = (o) => (int)byteGetter(o);
                        break;
                    case JSDataSourceSchemaType.DecimalValue:
                        decimalGetter = (Func<object, decimal>)valueGetter;
                        floatingPointGetter = (o) => (double)decimalGetter(o);
                        break;  
                    case JSDataSourceSchemaType.IntValue:
                        integerGetter = (Func<object, int>)valueGetter;
                        break;
                    case JSDataSourceSchemaType.ShortValue:
                        shortGetter = (Func<object, short>)valueGetter;
                        integerGetter = (o) => (int)shortGetter(o);
                        break;
                    case JSDataSourceSchemaType.LongValue:
                        longGetter = (Func<object, long>)valueGetter;
                        break;
                    case JSDataSourceSchemaType.StringValue:
                        if (isIDColumn)
                        {
                            idGetter = (Func<object, Guid>)valueGetter;
                            stringGetter = (o) => idGetter(o).ToString();
                        }
                        else
                        {
                            stringGetter = (Func<object, string>)valueGetter;
                        }
                        break;
                    case JSDataSourceSchemaType.CalendarValue:
                    case JSDataSourceSchemaType.DateTimeValue:
                        if (typeof(Func<object, DateTime>).IsAssignableFrom(valueGetter.GetType()))
                        {
                            dateTimeGetter = (Func<object, DateTime>)valueGetter;
                            stringGetter = (o) => ((DateTime)dateTimeGetter(o)).ToString("o");
                        }
                        else
                        {
                            dateTimeGetter = (o) => (DateTime)untypedGetter(o);
                            stringGetter = (o) => ((DateTime)dateTimeGetter(o)).ToString("o");
                        }
                        break;
                    case JSDataSourceSchemaType.ObjectValue:
                        objectGetter = untypedGetter;
                        break;

                    case JSDataSourceSchemaType.DoubleArrayValue:
                    case JSDataSourceSchemaType.IntArrayValue:
                    case JSDataSourceSchemaType.LongArrayValue:
                    case JSDataSourceSchemaType.StringArrayValue:
                    case JSDataSourceSchemaType.CalendarArrayValue:
                    case JSDataSourceSchemaType.DateTimeArrayValue:
                    case JSDataSourceSchemaType.BooleanArrayValue:
                    case JSDataSourceSchemaType.DecimalArrayValue:
                    case JSDataSourceSchemaType.ByteArrayValue:
                    case JSDataSourceSchemaType.ShortArrayValue:
                    case JSDataSourceSchemaType.SingleArrayValue:
                        objectGetter = untypedGetter;
                        break;

                    case JSDataSourceSchemaType.NullableDoubleValue:
                        nullableDoubleGetter = (Func<object, double?>)valueGetter;
                        nullableFloatingPointGetter = nullableDoubleGetter;
                        break;
                    case JSDataSourceSchemaType.NullableSingleValue:
                        nullableSingleGetter = (Func<object, float?>)valueGetter;
                        nullableFloatingPointGetter = (o) => (double?)nullableSingleGetter(o);
                        break;
                    case JSDataSourceSchemaType.NullableBooleanValue:
                        nullableBoolGetter = (Func<object, bool?>)valueGetter;
                        nullableIntegerGetter = (o) =>
                        {
                            var val = nullableBoolGetter(o);
                            int? t = 1;
                            int? f = 0;
                            return val == null ? null : (val == true) ? t : f;
                        };
                        break;
                    case JSDataSourceSchemaType.NullableByteValue:
                        nullableByteGetter = (Func<object, byte?>)valueGetter;
                        nullableIntegerGetter = (o) => (int?)nullableByteGetter(o);
                        break;
                    case JSDataSourceSchemaType.NullableDecimalValue:
                        nullableDecimalGetter = (Func<object, decimal?>)valueGetter;
                        nullableFloatingPointGetter = (o) => (double?)nullableDecimalGetter(o);
                        break;
                    case JSDataSourceSchemaType.NullableIntValue:
                        nullableIntegerGetter = (Func<object, int?>)valueGetter;
                        break;
                    case JSDataSourceSchemaType.NullableShortValue:
                        nullableShortGetter = (Func<object, short?>)valueGetter;
                        nullableIntegerGetter = (o) => (int?)nullableShortGetter(o);
                        break;
                    case JSDataSourceSchemaType.NullableLongValue:
                        nullableLongGetter = (Func<object, long?>)valueGetter;
                        break;
                    case JSDataSourceSchemaType.NullableCalendarValue:
                    case JSDataSourceSchemaType.NullableDateTimeValue:
                        if (typeof(Func<object, DateTime?>).IsAssignableFrom(valueGetter.GetType()))
                        {
                            nullableDateTimeGetter = (Func<object, DateTime?>)valueGetter;
                            stringGetter = (o) =>
                            {
                                var val = nullableDateTimeGetter(o);
                                return val == null ? null : val.Value.ToString("o");
                            };
                        }
                        else
                        {
                            nullableDateTimeGetter = (o) => (DateTime?)untypedGetter(o);
                            stringGetter = (o) =>
                            {
                                var val = nullableDateTimeGetter(o);
                                return val == null ? null : val.Value.ToString("o");
                            };
                        }
                        break;
                }  

                Action<int, UnmarshalledColumnData, int, object> insert = null;
                switch (newColumn.Type)
                {
                    case JSDataSourceSchemaType.DoubleValue:
                    case JSDataSourceSchemaType.SingleValue:
                    case JSDataSourceSchemaType.DecimalValue:
                        insert = (size, column, index, item) =>
                        {
                            double floatVal = double.NaN;
                            if (item != null)
                            {
                                if (!schema.IsPrimitive)
                                {
                                    floatVal = floatingPointGetter(item);
                                } else
                                {
                                    floatVal = Convert.ToDouble(item);
                                }
                            }
                            if (index == size)
                            {
                                column.DoubleValues[index] = floatVal;                               
                            } 
                            else 
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.DoubleValues, index, column.DoubleValues, index + 1, size - index);
                                column.DoubleValues[index] = floatVal;
                            }                      
                        };
                        break;
                    case JSDataSourceSchemaType.NullableDoubleValue:
                    case JSDataSourceSchemaType.NullableSingleValue:
                    case JSDataSourceSchemaType.NullableDecimalValue:
                        insert = (size, column, index, item) =>
                        {
                            double? floatVal = null;
                            if (item != null)
                            {
                                floatVal = nullableFloatingPointGetter(item);
                            }
                            if (index == size)
                            {
                                column.DoubleValues[index] = floatVal != null ? floatVal.Value : double.NaN;
                                column.NullValues[index] = floatVal == null;
                            }
                            else
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.DoubleValues, index, column.DoubleValues, index + 1, size - index);
                                Array.Copy(column.NullValues, index, column.NullValues, index + 1, size - index);
                                column.DoubleValues[index] = floatVal != null ? floatVal.Value : double.NaN;
                                column.NullValues[index] = floatVal == null;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.BooleanValue:
                    case JSDataSourceSchemaType.ByteValue:
                    case JSDataSourceSchemaType.IntValue:
                    case JSDataSourceSchemaType.ShortValue:
                        insert = (size, column, index, item) =>
                        {
                            int intVal = int.MinValue;
                            if (item != null)
                            {
                                if (!schema.IsPrimitive) 
                                {
                                    intVal = integerGetter(item);
                                }
                                else 
                                {
                                    intVal = Convert.ToInt32(item);
                                }
                            }
                            if (index == size)
                            {
                                column.IntValues[index] = intVal;                               
                            } 
                            else 
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.IntValues, index, column.IntValues, index + 1, size - index);
                                column.IntValues[index] = intVal;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.NullableBooleanValue:
                    case JSDataSourceSchemaType.NullableByteValue:
                    case JSDataSourceSchemaType.NullableIntValue:
                    case JSDataSourceSchemaType.NullableShortValue:
                        insert = (size, column, index, item) =>
                        {
                            int? intVal = null;
                            if (item != null)
                            {
                                intVal = nullableIntegerGetter(item);
                            }
                            if (index == size)
                            {
                                column.IntValues[index] = intVal != null ? intVal.Value : int.MinValue;
                                column.NullValues[index] = intVal == null;
                            }
                            else
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.IntValues, index, column.IntValues, index + 1, size - index);
                                Array.Copy(column.NullValues, index, column.NullValues, index + 1, size - index);
                                column.IntValues[index] = intVal != null ? intVal.Value : int.MinValue;
                                column.NullValues[index] = intVal == null;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.LongValue:
                        insert = (size, column, index, item) =>
                        {
                            long longVal = long.MinValue;
                            if (item != null)
                            {
                                if (!schema.IsPrimitive)
                                {
                                    longVal = longGetter(item);
                                }
                                else
                                {
                                    longVal = (long)item;
                                }
                            }
                            if (index == size)
                            {
                                column.LongValues[index] = longVal;                               
                            } 
                            else 
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.LongValues, index, column.LongValues, index + 1, size - index);
                                column.LongValues[index] = longVal;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.NullableLongValue:
                        insert = (size, column, index, item) =>
                        {
                            long? longVal = null;
                            if (item != null)
                            {
                                longVal = nullableLongGetter(item);
                            }
                            if (index == size)
                            {
                                column.LongValues[index] = longVal != null ? longVal.Value : long.MinValue;
                                column.NullValues[index] = longVal == null;
                            }
                            else
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.LongValues, index, column.LongValues, index + 1, size - index);
                                Array.Copy(column.NullValues, index, column.NullValues, index + 1, size - index);
                                column.LongValues[index] = longVal != null ? longVal.Value : long.MinValue;
                                column.NullValues[index] = longVal == null;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.StringValue:
                    case JSDataSourceSchemaType.CalendarValue:
                    case JSDataSourceSchemaType.DateTimeValue:
                        insert = (size, column, index, item) => 
                        {
                            string stringVal = null;
                            Guid idVal = Guid.Empty;
                            if (item != null)
                            {
                                if (column.IsIDColumn)
                                {
                                    idVal = idGetter(item);
                                    stringVal = _parentId != null ? _parentId + "/" + idVal.ToString() : idVal.ToString();
                                }
                                else if (!schema.IsPrimitive)
                                {
                                    try
                                    {
                                        stringVal = stringGetter(item);
                                    }
                                    catch
                                    {
                                        stringVal = null;
                                    }
                                }
                                else
                                {
                                    stringVal = item.ToString();
                                }
                            }
                            if (index == size)
                            {
                                if (column.StringValues == null)
                                {
                                    //Console.WriteLine("stringvalues null: " + column.PropertyName);
                                }
                                column.StringValues[index] = stringVal;                               
                            } 
                            else 
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.StringValues, index, column.StringValues, index + 1, size - index);
                                column.StringValues[index] = stringVal;
                            }

                            if (column.IsIDColumn)
                            {
                                if (index == size)
                                {
                                    if (column.IDValues == null)
                                    {
                                        //Console.WriteLine("stringvalues null: " + column.PropertyName);
                                    }
                                    column.IDValues[index] = idVal;                               
                                } 
                                else 
                                {
                                    //Console.WriteLine("shouldn't be here");
                                    Array.Copy(column.IDValues, index, column.IDValues, index + 1, size - index);
                                    column.IDValues[index] = idVal;
                                }
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.NullableCalendarValue:
                    case JSDataSourceSchemaType.NullableDateTimeValue:
                        insert = (size, column, index, item) =>
                        {
                            string stringVal = null;
                            if (item != null)
                            {
                                try
                                {
                                    stringVal = stringGetter(item);
                                }
                                catch
                                {
                                    stringVal = null;
                                }
                            }
                            if (index == size)
                            {
                                if (column.StringValues == null)
                                {
                                    //Console.WriteLine("stringvalues null: " + column.PropertyName);
                                }
                                column.StringValues[index] = stringVal;
                            }
                            else
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.StringValues, index, column.StringValues, index + 1, size - index);
                                column.StringValues[index] = stringVal;
                            }
                        };
                        break;
                case JSDataSourceSchemaType.ObjectValue:
                        insert = (size, column, index, item) => 
                        {
                            //Console.WriteLine("shouldn't be here");
                            object objVal = null;
                            if (item != null)
                            {
                                objVal = objectGetter(item);
                            }
                            if (objVal != null && column.SubColumns == null)
                            {
                                var subSchema = schema.GetSubSchema(column.PropertyName);
                                if (subSchema == null)
                                {
                                    subSchema = ExtractSchema(objVal);
                                    schema.SetSubSchema(column.PropertyName, subSchema);
                                    column.SubSchema = subSchema;
                                }
                                if (subSchema.IsDataSource && !column.IsSubDataSource)
                                {
                                    column.IsSubDataSource = true;
                                    var c = column.Column;
                                    c.IsSubDataSource = column.IsSubDataSource ? 1 : 0;
                                    column.Column = c;
                                    column = AdjustColumnCapacity(parentPath, column, schema, column.PropertyName, valueGetter, untypedGetter, column.IsIDColumn, column.Type, Capacity, Capacity);
                                }
                                else
                                {
                                    column.SubColumns = AdjustCapacity(column.PropertyPath, column.SubColumns, subSchema, Capacity, Capacity);
                                }
                            }

                            if (column.IsSubDataSource)
                            {
                                UnmarshalledColumn[] cols = null;
                                if (objVal != null)
                                {
                                    var id = _idGetter(item);
                                    var parentId = _parentId != null ? _parentId + "/" + id.ToString() : id.ToString();
                                    
                                    var sub = (UnmarshalledDataSource)UnmarshalledDataSource.CreateWithSchema(objVal, parentId, column.SubSchema, _manager, _helper);
                                    cols = sub.GetColumns("");

                                    if (!_subDataSources.ContainsKey(id))
                                    {
                                        _subDataSources[id] = new Dictionary<string, UnmarshalledDataSource>();
                                    }
                                    _subDataSources[id].Add(column.PropertyName, sub);
                                }
                                if (index == size)
                                {
                                    column.SubDataSourceValues[index] = cols;                               
                                } 
                                else 
                                {
                                    //Console.WriteLine("shouldn't be here");
                                    Array.Copy(column.SubDataSourceValues, index, column.SubDataSourceValues, index + 1, size - index);
                                    column.SubDataSourceValues[index] = cols;
                                }
                            }
                            else
                            {
                                if (column.SubColumns != null)
                                {
                                    for (var i = 0; i < column.SubColumns.Length; i++)
                                    {
                                        var subColumn = column.SubColumns[i];
                                        subColumn.Insert(size, subColumn, index, objVal);
                                    }
                                }
                            }
                        };
                        break;
                case JSDataSourceSchemaType.DoubleArrayValue:
                case JSDataSourceSchemaType.IntArrayValue:
                case JSDataSourceSchemaType.LongArrayValue:
                case JSDataSourceSchemaType.StringArrayValue:
                case JSDataSourceSchemaType.CalendarArrayValue:
                case JSDataSourceSchemaType.DateTimeArrayValue:
                case JSDataSourceSchemaType.BooleanArrayValue:
                case JSDataSourceSchemaType.DecimalArrayValue:
                case JSDataSourceSchemaType.ByteArrayValue:
                case JSDataSourceSchemaType.ShortArrayValue:
                case JSDataSourceSchemaType.SingleArrayValue:
                    insert = (size, column, index, item) =>
                    {
                        object objVal = null;
                        if (item != null)
                        {
                            objVal = objectGetter(item);
                        }
                        if (objVal != null && column.SubColumns == null)
                        {
                            var subSchema = schema.GetSubSchema(column.PropertyName);
                            if (subSchema == null)
                            {
                                subSchema = ExtractSchema(objVal);
                                

                                schema.SetSubSchema(column.PropertyName, subSchema);
                                column.SubSchema = subSchema;
                            }
                            if (subSchema.IsDataSource && !column.IsSubDataSource)
                            {
                                column.IsSubDataSource = true;
                                var c = column.Column;
                                c.IsSubDataSource = column.IsSubDataSource ? 1 : 0;
                                column.Column = c;
                                column = AdjustColumnCapacity(parentPath, column, schema, column.PropertyName, valueGetter, untypedGetter, column.IsIDColumn, column.Type, Capacity, Capacity);
                            }
                            else
                            {
                                column.SubColumns = AdjustCapacity(column.PropertyPath, column.SubColumns, subSchema, Capacity, Capacity);
                            }
                        }

                        if (column.IsSubDataSource)
                        {
                            UnmarshalledColumn[] cols = null;
                            if (objVal != null)
                            {
                                var sub = (UnmarshalledDataSource)UnmarshalledDataSource.CreateWithSchema(objVal, column.SubSchema, _manager, _helper);
                                var subcols = sub.GetColumns("");

                                UnmarshalledColumn primcol = new UnmarshalledColumn();
                                primcol.ActualCount = subcols[0].ActualCount;
                                primcol.DataSourceID = subcols[0].DataSourceID;
                                primcol.PropertyPath = "___primitiveVal";
                                primcol.Type = GetArrayType(newColumn.Type);
                                int i = 0;
                                switch (newColumn.Type)
                                {
                                    case JSDataSourceSchemaType.StringArrayValue:
                                        primcol.StringValues = new string[primcol.ActualCount];
                                        foreach (var v in (objVal as IEnumerable))
                                        {
                                            primcol.StringValues[i] = (string)v;
                                            i++;
                                        }
                                        break;
                                    case JSDataSourceSchemaType.DateTimeArrayValue:
                                    case JSDataSourceSchemaType.CalendarArrayValue:
                                        primcol.StringValues = new string[primcol.ActualCount];
                                        foreach (var v in (objVal as IEnumerable))
                                        {
                                            primcol.StringValues[i] = ((DateTime)v).ToString("o");
                                            i++;
                                        }
                                        break;
                                    case JSDataSourceSchemaType.BooleanArrayValue:
                                    case JSDataSourceSchemaType.ByteArrayValue:
                                    case JSDataSourceSchemaType.IntArrayValue:
                                    case JSDataSourceSchemaType.ShortArrayValue:
                                        primcol.IntValues = new int[primcol.ActualCount];
                                        foreach (var v in (objVal as IEnumerable))
                                        {
                                            primcol.IntValues[i] = Convert.ToInt32(v);
                                            i++;
                                        }
                                        break;
                                    case JSDataSourceSchemaType.DoubleArrayValue:
                                    case JSDataSourceSchemaType.SingleArrayValue:
                                    case JSDataSourceSchemaType.DecimalArrayValue:
                                        primcol.DoubleValues = new double[primcol.ActualCount];
                                        foreach (var v in (objVal as IEnumerable))
                                        {
                                            primcol.DoubleValues[i] = Convert.ToDouble(v);
                                            i++;
                                        }
                                        break;
                                    case JSDataSourceSchemaType.LongArrayValue:
                                        primcol.LongValues = new long[primcol.ActualCount];
                                        foreach (var v in (objVal as IEnumerable))
                                        {
                                            primcol.LongValues[i] = Convert.ToInt64(v);
                                            i++;
                                        }
                                        break;
                                }

                                cols = new UnmarshalledColumn[subcols.Length + 1];
                                for (i = 0; i < subcols.Length; i++)
                                {
                                    cols[i] = subcols[i];
                                }
                                cols[subcols.Length] = primcol;
                            }
                            if (index == size)
                            {
                                column.SubDataSourceValues[index] = cols;
                            }
                            else
                            {
                                //Console.WriteLine("shouldn't be here");
                                Array.Copy(column.SubDataSourceValues, index, column.SubDataSourceValues, index + 1, size - index);
                                column.SubDataSourceValues[index] = cols;
                            }
                        }
                        else
                        {
                            if (column.SubColumns != null)
                            {
                                for (var i = 0; i < column.SubColumns.Length; i++)
                                {
                                    var subColumn = column.SubColumns[i];
                                    subColumn.Insert(size, subColumn, index, objVal);
                                }
                            }
                        }
                    };
                    break;
                }

            Action<int, UnmarshalledColumnData, int, object, object> update = null;
                switch (newColumn.Type)
                {
                    case JSDataSourceSchemaType.DoubleValue:
                    case JSDataSourceSchemaType.SingleValue:
                    case JSDataSourceSchemaType.DecimalValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            double floatVal = double.NaN;
                            if (newItem != null)
                            {
                                floatVal = floatingPointGetter(newItem);
                            }
                            column.DoubleValues[index] = floatVal;                      
                        };
                        break;
                    case JSDataSourceSchemaType.NullableDoubleValue:
                    case JSDataSourceSchemaType.NullableSingleValue:
                    case JSDataSourceSchemaType.NullableDecimalValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            double? floatVal = null;
                            if (newItem != null)
                            {
                                floatVal = nullableFloatingPointGetter(newItem);
                            }
                            column.DoubleValues[index] = floatVal != null ? floatVal.Value : double.NaN;
                            column.NullValues[index] = floatVal == null;
                        };
                        break;
                    case JSDataSourceSchemaType.BooleanValue:
                    case JSDataSourceSchemaType.ByteValue:
                    case JSDataSourceSchemaType.IntValue:
                    case JSDataSourceSchemaType.ShortValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            int intVal = int.MinValue;
                            if (newItem != null)
                            {
                                intVal = integerGetter(newItem);
                            }
                            column.IntValues[index] = intVal;                            
                        };
                        break;
                    case JSDataSourceSchemaType.NullableBooleanValue:
                    case JSDataSourceSchemaType.NullableByteValue:
                    case JSDataSourceSchemaType.NullableIntValue:
                    case JSDataSourceSchemaType.NullableShortValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            int? intVal = null;
                            if (newItem != null)
                            {
                                intVal = nullableIntegerGetter(newItem);
                            }
                            column.IntValues[index] = intVal != null ? intVal.Value : int.MinValue;
                            column.NullValues[index] = intVal == null;
                        };
                        break;
                    case JSDataSourceSchemaType.LongValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            long longVal = long.MinValue;
                            if (newItem != null)
                            {
                                longVal = longGetter(newItem);
                            }
                            column.LongValues[index] = longVal;
                        };
                        break;
                    case JSDataSourceSchemaType.NullableLongValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            long? longVal = null;
                            if (newItem != null)
                            {
                                longVal = nullableLongGetter(newItem);
                            }
                            column.LongValues[index] = longVal != null ? longVal.Value : long.MinValue;
                            column.NullValues[index] = longVal == null;
                        };
                        break;
                    case JSDataSourceSchemaType.StringValue:
                    case JSDataSourceSchemaType.CalendarValue:
                    case JSDataSourceSchemaType.DateTimeValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            string stringVal = null;
                            Guid idVal = Guid.Empty;
                            if (column.IsIDColumn && oldItem != newItem)
                            {
                                var oldId = column.IDValues[index];
                                OnRemoveId(oldId);
                            }
                            if (newItem != null)
                            {
                                if (column.IsIDColumn)
                                {
                                    idVal = idGetter(newItem);
                                    stringVal = idVal.ToString();
                                }
                                else
                                {
                                    stringVal = stringGetter(newItem);
                                }
                            }
                           
                            column.StringValues[index] = stringVal;
                            if (column.IsIDColumn)
                            {
                                column.IDValues[index] = idVal;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.NullableCalendarValue:
                    case JSDataSourceSchemaType.NullableDateTimeValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            string stringVal = null;
                            if (newItem != null)
                            {
                                stringVal = stringGetter(newItem);
                            }
                            column.StringValues[index] = stringVal;
                        };
                        break;
                    case JSDataSourceSchemaType.ObjectValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            object objVal = null;
                            if (newItem != null)
                            {
                                objVal = objectGetter(newItem);
                            }

                            object oldObjVal = null;
                            if (oldItem != null)
                            {
                                oldObjVal = objectGetter(oldItem);
                            }

                            if (objVal != null && column.SubColumns == null)
                            {
                                var subSchema = schema.GetSubSchema(column.PropertyName);
                                if (subSchema == null)
                                {
                                    subSchema = ExtractSchema(objVal);
                                    schema.SetSubSchema(column.PropertyName, subSchema);
                                    column.SubSchema = subSchema;
                                }
                                if (subSchema.IsDataSource && !column.IsSubDataSource)
                                {
                                    column.IsSubDataSource = true;
                                    var c = column.Column;
                                    c.IsSubDataSource = column.IsSubDataSource ? 1 : 0;
                                    column.Column = c;
                                    column = AdjustColumnCapacity(parentPath, column, schema, column.PropertyName, valueGetter, untypedGetter, false, column.Type, Capacity, Capacity);
                                }
                                
                                column.SubColumns = AdjustCapacity(column.PropertyPath, column.SubColumns, subSchema, Capacity, Capacity);
                            }

                            if (column.IsSubDataSource)
                            {
                                UnmarshalledColumn[] cols = null;
                                if (objVal != null)
                                {
                                    var sub = (UnmarshalledDataSource)UnmarshalledDataSource.CreateWithSchema(objVal, column.SubSchema, _manager, _helper);
                                    cols = sub.GetColumns("");
                                }
                                column.SubDataSourceValues[index] = cols;
                            }
                            else
                            {
                                if (column.SubColumns != null)
                                {
                                    for (var i = 0; i < column.SubColumns.Length; i++)
                                    {
                                        var subColumn = column.SubColumns[i];
                                        subColumn.Update(size, subColumn, index, oldObjVal, objVal);
                                    }
                                }
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.DoubleArrayValue:
                    case JSDataSourceSchemaType.IntArrayValue:
                    case JSDataSourceSchemaType.LongArrayValue:
                    case JSDataSourceSchemaType.StringArrayValue:
                    case JSDataSourceSchemaType.CalendarArrayValue:
                    case JSDataSourceSchemaType.DateTimeArrayValue:
                    case JSDataSourceSchemaType.BooleanArrayValue:
                    case JSDataSourceSchemaType.DecimalArrayValue:
                    case JSDataSourceSchemaType.ByteArrayValue:
                    case JSDataSourceSchemaType.ShortArrayValue:
                    case JSDataSourceSchemaType.SingleArrayValue:
                        update = (size, column, index, oldItem, newItem) =>
                        {
                            object objVal = null;
                            if (newItem != null)
                            {
                                objVal = objectGetter(newItem);
                            }

                            object oldObjVal = null;
                            if (oldItem != null)
                            {
                                oldObjVal = objectGetter(oldItem);
                            }

                            if (column.IsSubDataSource)
                            {
                                UnmarshalledColumn[] cols = null;
                                if (objVal != null)
                                {
                                    var sub = (UnmarshalledDataSource)UnmarshalledDataSource.CreateWithSchema(objVal, column.SubSchema, _manager, _helper);
                                    var subcols = sub.GetColumns("");

                                    UnmarshalledColumn primcol = new UnmarshalledColumn();
                                    primcol.ActualCount = subcols[0].ActualCount;
                                    primcol.DataSourceID = subcols[0].DataSourceID;
                                    primcol.PropertyPath = "___primitiveVal";
                                    primcol.Type = GetArrayType(newColumn.Type);
                                    int i = 0;
                                    switch (newColumn.Type)
                                    {
                                        case JSDataSourceSchemaType.StringArrayValue:
                                            primcol.StringValues = new string[primcol.ActualCount];
                                            foreach (var v in (objVal as IEnumerable))
                                            {
                                                primcol.StringValues[i] = (string)v;
                                                i++;
                                            }
                                            break;
                                        case JSDataSourceSchemaType.DateTimeArrayValue:
                                        case JSDataSourceSchemaType.CalendarArrayValue:
                                            primcol.StringValues = new string[primcol.ActualCount];
                                            foreach (var v in (objVal as IEnumerable))
                                            {
                                                primcol.StringValues[i] = ((DateTime)v).ToString("o");
                                                i++;
                                            }
                                            break;
                                        case JSDataSourceSchemaType.BooleanArrayValue:
                                        case JSDataSourceSchemaType.ByteArrayValue:
                                        case JSDataSourceSchemaType.IntArrayValue:
                                        case JSDataSourceSchemaType.ShortArrayValue:
                                            primcol.IntValues = new int[primcol.ActualCount];
                                            foreach (var v in (objVal as IEnumerable))
                                            {
                                                primcol.IntValues[i] = Convert.ToInt32(v);
                                                i++;
                                            }
                                            break;
                                        case JSDataSourceSchemaType.DoubleArrayValue:
                                        case JSDataSourceSchemaType.SingleArrayValue:
                                        case JSDataSourceSchemaType.DecimalArrayValue:
                                            primcol.DoubleValues = new double[primcol.ActualCount];
                                            foreach (var v in (objVal as IEnumerable))
                                            {
                                                primcol.DoubleValues[i] = Convert.ToDouble(v);
                                                i++;
                                            }
                                            break;
                                        case JSDataSourceSchemaType.LongArrayValue:
                                            primcol.LongValues = new long[primcol.ActualCount];
                                            foreach (var v in (objVal as IEnumerable))
                                            {
                                                primcol.LongValues[i] = Convert.ToInt64(v);
                                                i++;
                                            }
                                            break;
                                    }

                                    cols = new UnmarshalledColumn[subcols.Length + 1];
                                    for (i = 0; i < subcols.Length; i++)
                                    {
                                        cols[i] = subcols[i];
                                    }
                                    cols[subcols.Length] = primcol;
                                }

                                column.SubDataSourceValues[index] = cols;
                            }
                            else
                            {
                                if (column.SubColumns != null)
                                {
                                    for (var i = 0; i < column.SubColumns.Length; i++)
                                    {
                                        var subColumn = column.SubColumns[i];
                                        subColumn.Update(size, subColumn, index, oldItem, newItem);
                                    }
                                }
                            }
                        };
                        break;
            }

            Action<int, UnmarshalledColumnData, int> remove = null;
                switch (newColumn.Type)
                {
                    case JSDataSourceSchemaType.DoubleValue:
                    case JSDataSourceSchemaType.SingleValue:
                    case JSDataSourceSchemaType.DecimalValue:
                        remove = (size, column, index) =>
                        {
                            if (index == (size - 1))
                            {
                                column.DoubleValues[index] = double.NaN;                               
                            } 
                            else    
                            {
                                Array.Copy(column.DoubleValues, index + 1, column.DoubleValues, index, (size - 1) - index);
                                column.DoubleValues[size - 1] = double.NaN;
                            }                      
                        };
                        break;
                    case JSDataSourceSchemaType.NullableDoubleValue:
                    case JSDataSourceSchemaType.NullableSingleValue:
                    case JSDataSourceSchemaType.NullableDecimalValue:
                        remove = (size, column, index) =>
                        {
                            if (index == (size - 1))
                            {
                                column.DoubleValues[index] = double.NaN;
                                column.NullValues[index] = false;
                            }
                            else
                            {
                                Array.Copy(column.DoubleValues, index + 1, column.DoubleValues, index, (size - 1) - index);
                                Array.Copy(column.NullValues, index + 1, column.NullValues, index, (size - 1) - index);
                                column.DoubleValues[size - 1] = double.NaN;
                                column.NullValues[size - 1] = false;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.BooleanValue:
                    case JSDataSourceSchemaType.ByteValue:
                    case JSDataSourceSchemaType.IntValue:
                    case JSDataSourceSchemaType.ShortValue:
                        remove = (size, column, index) =>
                        {
                            if (index == (size - 1))
                            {
                                column.IntValues[index] = 0;                               
                            } 
                            else    
                            {
                                Array.Copy(column.IntValues, index + 1, column.IntValues, index, (size - 1) - index);
                                column.IntValues[size - 1] = 0;
                            }                      
                        };
                        break;
                    case JSDataSourceSchemaType.NullableBooleanValue:
                    case JSDataSourceSchemaType.NullableByteValue:
                    case JSDataSourceSchemaType.NullableIntValue:
                    case JSDataSourceSchemaType.NullableShortValue:
                        remove = (size, column, index) =>
                        {
                            if (index == (size - 1))
                            {
                                column.IntValues[index] = 0;
                                column.NullValues[index] = false;
                            }
                            else
                            {
                                Array.Copy(column.IntValues, index + 1, column.IntValues, index, (size - 1) - index);
                                Array.Copy(column.NullValues, index + 1, column.NullValues, index, (size - 1) - index);
                                column.IntValues[size - 1] = 0;
                                column.NullValues[size - 1] = false;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.LongValue:
                        remove = (size, column, index) =>
                        {
                            if (index == (size - 1))
                            {
                                column.LongValues[index] = 0;                               
                            } 
                            else    
                            {
                                Array.Copy(column.LongValues, index + 1, column.LongValues, index, (size - 1) - index);
                                column.LongValues[size - 1] = 0;
                            }                      
                        };
                        break;
                    case JSDataSourceSchemaType.NullableLongValue:
                        remove = (size, column, index) =>
                        {
                            if (index == (size - 1))
                            {
                                column.LongValues[index] = 0;
                                column.NullValues[index] = false;
                            }
                            else
                            {
                                Array.Copy(column.LongValues, index + 1, column.LongValues, index, (size - 1) - index);
                                Array.Copy(column.NullValues, index + 1, column.NullValues, index, (size - 1) - index);
                                column.LongValues[size - 1] = 0;
                                column.NullValues[size - 1] = false;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.StringValue:
                    case JSDataSourceSchemaType.CalendarValue:
                    case JSDataSourceSchemaType.DateTimeValue:
                        remove = (size, column, index) =>
                        {
                            if (column.IsIDColumn)
                            {
                                var oldId = column.IDValues[index];
                                OnRemoveId(oldId);
                            }

                            if (index == (size - 1))
                            {
                                column.StringValues[index] = null;                               
                            } 
                            else    
                            {
                                Array.Copy(column.StringValues, index + 1, column.StringValues, index, (size - 1) - index);
                                column.StringValues[size - 1] = null;
                            }
                            if (column.IsIDColumn)
                            {
                                if (index == (size - 1))
                                {
                                    column.IDValues[index] = Guid.Empty;                               
                                } 
                                else    
                                {
                                    Array.Copy(column.IDValues, index + 1, column.IDValues, index, (size - 1) - index);
                                    column.IDValues[size - 1] = Guid.Empty;
                                }
                            }                      
                        };
                        break;
                    case JSDataSourceSchemaType.NullableCalendarValue:
                    case JSDataSourceSchemaType.NullableDateTimeValue:
                        remove = (size, column, index) =>
                        {
                            if (index == (size - 1))
                            {
                                column.StringValues[index] = null;
                            }
                            else
                            {
                                Array.Copy(column.StringValues, index + 1, column.StringValues, index, (size - 1) - index);
                                column.StringValues[size - 1] = null;
                            }
                        };
                        break;
                    case JSDataSourceSchemaType.ObjectValue:
                    case JSDataSourceSchemaType.DoubleArrayValue:
                    case JSDataSourceSchemaType.IntArrayValue:
                    case JSDataSourceSchemaType.LongArrayValue:
                    case JSDataSourceSchemaType.StringArrayValue:
                    case JSDataSourceSchemaType.CalendarArrayValue:
                    case JSDataSourceSchemaType.DateTimeArrayValue:
                    case JSDataSourceSchemaType.BooleanArrayValue:
                    case JSDataSourceSchemaType.DecimalArrayValue:
                    case JSDataSourceSchemaType.ByteArrayValue:
                    case JSDataSourceSchemaType.ShortArrayValue:
                    case JSDataSourceSchemaType.SingleArrayValue:
                        remove = (size, column, index) =>
                        {
                            if (column.IsSubDataSource)
                            {
                                if (index == (size - 1))
                                {
                                    column.SubDataSourceValues[index] = null;                               
                                } 
                                else    
                                {
                                    Array.Copy(column.SubDataSourceValues, index + 1, column.SubDataSourceValues, index, (size - 1) - index);
                                    column.SubDataSourceValues[size - 1] = null;
                                } 
                            }
                            else
                            {
                                if (column.SubColumns != null)
                                {
                                    for (var i = 0; i < column.SubColumns.Length; i++)
                                    {
                                        var subColumn = column.SubColumns[i];
                                        subColumn.Remove(size, subColumn, index);
                                    }
                                }
                            }
                        };
                        break;
                }  

             Action<int, UnmarshalledColumnData> clear = null;
                switch (newColumn.Type)
                {
                    case JSDataSourceSchemaType.DoubleValue:
                    case JSDataSourceSchemaType.SingleValue:
                    case JSDataSourceSchemaType.DecimalValue:
                        clear = (size, column) =>
                        {
                            Array.Clear(column.DoubleValues, 0, size);                  
                        };
                        break;
                    case JSDataSourceSchemaType.NullableDoubleValue:
                    case JSDataSourceSchemaType.NullableSingleValue:
                    case JSDataSourceSchemaType.NullableDecimalValue:
                        clear = (size, column) =>
                        {
                            Array.Clear(column.DoubleValues, 0, size);
                            Array.Clear(column.NullValues, 0, size);
                        };
                        break;
                    case JSDataSourceSchemaType.BooleanValue:
                    case JSDataSourceSchemaType.ByteValue:
                    case JSDataSourceSchemaType.IntValue:
                    case JSDataSourceSchemaType.ShortValue:
                        clear = (size, column) =>
                        {
                            Array.Clear(column.IntValues, 0, size);                  
                        };
                        break;
                    case JSDataSourceSchemaType.NullableBooleanValue:
                    case JSDataSourceSchemaType.NullableByteValue:
                    case JSDataSourceSchemaType.NullableIntValue:
                    case JSDataSourceSchemaType.NullableShortValue:
                        clear = (size, column) =>
                        {
                            Array.Clear(column.IntValues, 0, size);
                            Array.Clear(column.NullValues, 0, size);
                        };
                        break;
                    case JSDataSourceSchemaType.LongValue:
                        clear = (size, column) =>
                        {
                            Array.Clear(column.LongValues, 0, size);                  
                        };
                        break;
                    case JSDataSourceSchemaType.NullableLongValue:
                        clear = (size, column) =>
                        {
                            Array.Clear(column.LongValues, 0, size);
                            Array.Clear(column.NullValues, 0, size);
                        };
                        break;
                    case JSDataSourceSchemaType.StringValue:
                    case JSDataSourceSchemaType.CalendarValue:
                    case JSDataSourceSchemaType.DateTimeValue:
                        clear = (size, column) =>
                        {
                            if (column.IsIDColumn)
                            {
                                for (var i = 0; i < size; i++)
                                {
                                    OnRemoveId(column.IDValues[i]);
                                }   
                            }

                            Array.Clear(column.StringValues, 0, size);
                            if (column.IsIDColumn)
                            {
                                Array.Clear(column.IDValues, 0, size);    
                            }              
                        };
                        break;
                    case JSDataSourceSchemaType.NullableCalendarValue:
                    case JSDataSourceSchemaType.NullableDateTimeValue:
                        clear = (size, column) =>
                        {
                            Array.Clear(column.StringValues, 0, size);
                        };
                        break;
                    case JSDataSourceSchemaType.ObjectValue:
                    case JSDataSourceSchemaType.DoubleArrayValue:
                    case JSDataSourceSchemaType.IntArrayValue:
                    case JSDataSourceSchemaType.LongArrayValue:
                    case JSDataSourceSchemaType.StringArrayValue:
                    case JSDataSourceSchemaType.CalendarArrayValue:
                    case JSDataSourceSchemaType.DateTimeArrayValue:
                    case JSDataSourceSchemaType.BooleanArrayValue:
                    case JSDataSourceSchemaType.DecimalArrayValue:
                    case JSDataSourceSchemaType.ByteArrayValue:
                    case JSDataSourceSchemaType.ShortArrayValue:
                    case JSDataSourceSchemaType.SingleArrayValue:
                        clear = (size, column) =>
                        {
                            if (column.IsSubDataSource)
                            {
                                Array.Clear(column.SubDataSourceValues, 0, size);          
                            }
                            else
                            {
                                if (column.SubColumns != null)
                                {
                                    for (var i = 0; i < column.SubColumns.Length; i++)
                                    {
                                        var subColumn = column.SubColumns[i];
                                        subColumn.Clear(size, subColumn);
                                    }
                                }
                            }
                        };
                        break;
                }

            newColumn.Insert = insert;
            newColumn.Update = update;
            newColumn.Remove = remove;
            newColumn.Clear = clear;
            
            return newColumn;
        }

        private JSDataSourceSchemaType GetArrayType(JSDataSourceSchemaType arrayType)
        {
            switch (arrayType)
            {
                case JSDataSourceSchemaType.BooleanArrayValue:
                    return JSDataSourceSchemaType.BooleanValue;
                case JSDataSourceSchemaType.ByteArrayValue:
                    return JSDataSourceSchemaType.ByteValue;
                case JSDataSourceSchemaType.CalendarArrayValue:
                    return JSDataSourceSchemaType.CalendarValue;
                case JSDataSourceSchemaType.DateTimeArrayValue:
                    return JSDataSourceSchemaType.DateTimeValue;
                case JSDataSourceSchemaType.DecimalArrayValue:
                    return JSDataSourceSchemaType.DecimalValue;
                case JSDataSourceSchemaType.DoubleArrayValue:
                    return JSDataSourceSchemaType.DoubleValue;
                case JSDataSourceSchemaType.IntArrayValue:
                    return JSDataSourceSchemaType.IntValue;
                case JSDataSourceSchemaType.LongArrayValue:
                    return JSDataSourceSchemaType.LongValue;
                case JSDataSourceSchemaType.ShortArrayValue:
                    return JSDataSourceSchemaType.ShortValue;
                case JSDataSourceSchemaType.SingleArrayValue:
                    return JSDataSourceSchemaType.SingleValue;
                case JSDataSourceSchemaType.StringArrayValue:
                    return JSDataSourceSchemaType.StringValue;
            }
            return JSDataSourceSchemaType.ObjectValue;
        }

        private void GetColumns(string refName, UnmarshalledColumnData[] columns, List<UnmarshalledColumn> l)
        {
            List<UnmarshalledColumnData[]> toDrill = new List<UnmarshalledColumnData[]>();

            if (columns == null)
            {
                return;
            }

            for (var i = 0; i < columns.Length; i++)
            {
                var curr = columns[i];
                if (curr != null)
                {
                    if (curr.SubSchema != null && !curr.IsSubDataSource)
                    {
                        if (curr.SubColumns != null)
                        {
                            toDrill.Add(curr.SubColumns);
                        }
                    }
                    else
                    {
                        var col = curr.Column;
                        col.DataSourceID = refName;
                        col.ActualCount = _size;
                        
                        l.Add(col);
                    }
                }
            }
            for (var i = 0; i < toDrill.Count; i++)
            {
                GetColumns(refName, toDrill[i], l);
            }
        }

        internal UnmarshalledColumn[] GetColumns(string refName)
        {
            var l = new List<UnmarshalledColumn>();
            GetColumns(refName, _columns, l);
            return l.ToArray();
        }

        public void SendClear(string containerId, string refName)
        {
            _helper.SendUnmarshalledColumnMessage("igUnmarshalledDataSourceClear", containerId + ":" + refName, -1, GetColumns(refName));
        }

        public void SendRemove(string containerId, string refName, int index)
        {
            _helper.SendUnmarshalledColumnMessage("igUnmarshalledDataSourceRemove", containerId + ":" + refName, index, GetColumns(refName));
        }

        public void SendInsert(string containerId, string refName, int index)
        {
            _helper.SendUnmarshalledColumnMessage("igUnmarshalledDataSourceInsert", containerId + ":" + refName, index, GetColumns(refName));
        }

        public void SendUpdate(string containerId, string refName, int index, bool syncDataOnly)
        {
            _helper.SendUnmarshalledColumnMessage("igUnmarshalledDataSourceUpdate", containerId + ":" + refName + ":" + (syncDataOnly ? "true" : "false"), index, GetColumns(refName));
        }

        public void SendCreate(string containerId, string refName, string dataIntents)
        {
            if (dataIntents != null)
            {
                //Console.WriteLine("sending create data intents");
                _helper.SendUnmarshalledColumnDataIntentsMessage("igUnmarshalledDataSourceCreateDataIntents", containerId + ":" + refName, dataIntents);    
            }
            _helper.SendUnmarshalledColumnMessage("igUnmarshalledDataSourceCreate", containerId + ":" + refName, -1, GetColumns(refName));
        }

        private void OnRemoveId(Guid oldId)
        {
            //if (_stringToUuid.ContainsKey(oldStringVal))
            {
                //var uuid = _stringToUuid[oldStringVal];
                if (_uuidToOriginal.ContainsKey(oldId))
                {
                    var orig = _uuidToOriginal[oldId];
                    if (_originalToUuid.ContainsKey(orig))
                    {
                        _originalToUuid.Remove(orig);
                    }
                    _uuidToOriginal.Remove(oldId);
                }
                //_stringToUuid.Remove(oldStringVal);
            }
        }

        private UnmarshalledColumnData AdjustColumnCapacity(string parentPath, UnmarshalledColumnData column, JSDataSourceSchema schema, string propertyName, Delegate getter, Func<object, object> untypedGetter, bool isIdColumn, JSDataSourceSchemaType type, int oldValue, int newValue)
        {
            if (column == null)
            {
                column = CreateColumn(parentPath, propertyName, schema, type, getter, untypedGetter, isIdColumn);
            }

            if (column.Type == JSDataSourceSchemaType.ObjectValue || (
                column.Type >= JSDataSourceSchemaType.DoubleArrayValue &&
                column.Type <= JSDataSourceSchemaType.SingleArrayValue))
            {
                if (column.IsSubDataSource)
                {
                    var existingColumn = column.SubDataSourceValues;
                    if (existingColumn == null || existingColumn.Length != newValue)
                    {
                        var subColumn = new UnmarshalledColumn[newValue][];
                        if (existingColumn != null)
                        {
                            Array.Copy(existingColumn, subColumn, _size);
                        }
                        column.SubDataSourceValues = subColumn;
                        var col = column.Column;
                        col.SubDataSourceValues = subColumn;
                        column.Column = col;
                    }
                }
                else
                {
                    var subSchema = schema.GetSubSchema(propertyName);
                    if (subSchema == null)
                    {
                        //Console.WriteLine("subschema is null: " + propertyName);
                    }
                    column.SubColumns = AdjustCapacity(parentPath + "." + propertyName, column.SubColumns, subSchema, oldValue, newValue);
                    column.SubSchema = subSchema;
                }
            }
            else
            {
                switch (type)
                {
                    case JSDataSourceSchemaType.DecimalValue:
                    case JSDataSourceSchemaType.DoubleValue:
                    case JSDataSourceSchemaType.SingleValue:
                        {
                            var existingColumn = column.DoubleValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var floatColumn = new double[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, floatColumn, _size);
                                }
                                column.DoubleValues = floatColumn;
                                var col = column.Column;
                                col.DoubleValues = floatColumn;
                                column.Column = col;
                            }
                        }
                        break;
                    case JSDataSourceSchemaType.NullableDecimalValue:
                    case JSDataSourceSchemaType.NullableDoubleValue:
                    case JSDataSourceSchemaType.NullableSingleValue:
                        {
                            var existingColumn = column.DoubleValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var floatColumn = new double[newValue];
                                var nullColumn = new bool[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, floatColumn, _size);
                                    Array.Copy(column.NullValues, nullColumn, _size);
                                }
                                column.DoubleValues = floatColumn;
                                column.NullValues = nullColumn;
                                var col = column.Column;
                                col.DoubleValues = floatColumn;
                                col.NullValues = nullColumn;
                                column.Column = col;
                            }
                        }
                        break;
                    case JSDataSourceSchemaType.BooleanValue:
                    case JSDataSourceSchemaType.ByteValue:
                    case JSDataSourceSchemaType.IntValue:
                    case JSDataSourceSchemaType.ShortValue:
                        {
                            var existingColumn = column.IntValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var intColumn = new int[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, intColumn, _size);
                                }
                                column.IntValues = intColumn;
                                var col = column.Column;
                                col.IntValues = intColumn;
                                column.Column = col;
                            }
                        }
                        break;
                    case JSDataSourceSchemaType.NullableBooleanValue:
                    case JSDataSourceSchemaType.NullableByteValue:
                    case JSDataSourceSchemaType.NullableIntValue:
                    case JSDataSourceSchemaType.NullableShortValue:
                        {
                            var existingColumn = column.IntValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var intColumn = new int[newValue];
                                var nullColumn = new bool[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, intColumn, _size);
                                    Array.Copy(column.NullValues, nullColumn, _size);
                                }
                                column.IntValues = intColumn;
                                column.NullValues = nullColumn;
                                var col = column.Column;
                                col.IntValues = intColumn;
                                col.NullValues = nullColumn;
                                column.Column = col;
                            }
                        }
                        break;
                    case JSDataSourceSchemaType.LongValue:
                        {
                            var existingColumn = column.LongValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var longColumn = new long[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, longColumn, _size);
                                }
                                column.LongValues = longColumn;
                                var col = column.Column;
                                col.LongValues = longColumn;
                                column.Column = col;
                            }
                        }
                        break;
                    case JSDataSourceSchemaType.NullableLongValue:
                        {
                            var existingColumn = column.LongValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var longColumn = new long[newValue];
                                var nullColumn = new bool[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, longColumn, _size);
                                    Array.Copy(column.NullValues, nullColumn, _size);
                                }
                                column.LongValues = longColumn;
                                column.NullValues = nullColumn;
                                var col = column.Column;
                                col.LongValues = longColumn;
                                col.NullValues = nullColumn;
                                column.Column = col;
                            }
                        }
                        break;
                    case JSDataSourceSchemaType.StringValue:
                    case JSDataSourceSchemaType.CalendarValue:
                    case JSDataSourceSchemaType.DateTimeValue:
                        {
                            var existingColumn = column.StringValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var stringColumn = new string[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, stringColumn, _size);
                                }
                                column.StringValues = stringColumn;
                                var col = column.Column;
                                col.StringValues = stringColumn;
                                column.Column = col;
                            }
                            if (column.IsIDColumn)
                            {
                                var existingIdColumn = column.IDValues;
                                if (existingIdColumn == null || existingIdColumn.Length != newValue)
                                {
                                    var idColumn = new Guid[newValue];
                                    if (existingIdColumn != null)
                                    {
                                        Array.Copy(existingIdColumn, idColumn, _size);
                                    }
                                    column.IDValues = idColumn;
                                }
                            }
                        }
                        break;
                    case JSDataSourceSchemaType.NullableCalendarValue:
                    case JSDataSourceSchemaType.NullableDateTimeValue:
                        {
                            var existingColumn = column.StringValues;
                            if (existingColumn == null || existingColumn.Length != newValue)
                            {
                                var stringColumn = new string[newValue];
                                if (existingColumn != null)
                                {
                                    Array.Copy(existingColumn, stringColumn, _size);
                                }
                                column.StringValues = stringColumn;
                                var col = column.Column;
                                col.StringValues = stringColumn;
                                column.Column = col;
                            }
                        }
                        break;
                }
            }

            return column;
        }

        private void EnsureCapacity(int required)
        {
            if (required > _capacity)
            {
                //Console.WriteLine("doubling capacity");
                int newCap = _capacity == 0 ? 4 : _capacity * 2;
                Capacity = newCap;
            }
        }

        public static IJSDataSource CreateWithSchema(Object data, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper) 
        {
            if (data == null) {
                return null;
            }
            
            if (data.GetType().IsArray) {
                return UnmarshalledDataSource.CreateFromArray((Array)data, schema, manager, helper);
                //return UnmarshalledDataSource.CreateFromArray((Object[])data, schema, manager, helper);
            }
            else if (data is IList) {
                return UnmarshalledDataSource.CreateFromIList((IList)data, schema, manager, helper);
            } else if (data is IEnumerable) {
                return UnmarshalledDataSource.CreateFromIEnumerable((IEnumerable)data, schema, manager, helper);
            }
            return null;
        }
        public static IJSDataSource CreateWithSchema(Object data, string parentId, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper)
        {
            if (data == null) {
                return null;
            }

            if (data.GetType().IsArray) {
                return UnmarshalledDataSource.CreateFromArray((Array)data, parentId, schema, manager, helper);
            }
            else if (data is IList) {
                return UnmarshalledDataSource.CreateFromIList((IList)data, parentId, schema, manager, helper);
            }
            else if (data is IEnumerable) {
                return UnmarshalledDataSource.CreateFromIEnumerable((IEnumerable)data, parentId, schema, manager, helper);
            }
            return null;
        }

        private void Listen(object data)
        {
            if (data is INotifyCollectionChanged)
            {
                _originalData = data;
                var i = (INotifyCollectionChanged)data;
                i.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SuppressModifications)
            {
                return;
            }
            
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
					if (e.NewItems != null)
					{
						for (var i = 0; i < e.NewItems.Count; i++)
						{
							var item = e.NewItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
                            _manager.NotifyInsertItem(refName , e.NewStartingIndex + i, item);
						}
					}
					break;
                }
				case NotifyCollectionChangedAction.Remove:
                {
					if (e.OldItems != null)
					{
						for (var i = 0; i < e.OldItems.Count; i++)
						{
							var item = e.OldItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
							_manager.NotifyRemoveItem(refName, e.OldStartingIndex, item);
						}
					}
					break;
                }
				case NotifyCollectionChangedAction.Replace:
                {
					if (e.OldItems != null)
					{
						for (var i = 0; i < e.OldItems.Count; i++)
						{
							var item = e.OldItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
							_manager.NotifyRemoveItem(refName, e.OldStartingIndex, item);
						}
					}
					if (e.NewItems != null)
					{
						for (var i = 0; i < e.NewItems.Count; i++)
						{
							var item = e.NewItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
							_manager.NotifyInsertItem(refName , e.NewStartingIndex + i, item);
						}
					}
					break;
                }
				case NotifyCollectionChangedAction.Reset:
                {
                    var refName = _manager.GetRefId(_originalData);
                    if (refName == null) {
                        return;
                    }
					_manager.NotifyClearItems(refName);
					break;
                }
            }
        }

        public bool HasId(Guid id)
        {
            return (_uuidToOriginal.ContainsKey(id));
        }
        public bool HasId(string id)
        {
            if (id.Contains("/"))
            {
                var parentIds = id.Split('/');
                var guid = Guid.Parse(parentIds[0]);
                if (_subDataSources.ContainsKey(guid))
                {
                    foreach (var data in _subDataSources[guid].Values)
                    {
                        if (data.HasId(id.Replace(parentIds[0] + "/", "")))
                            return true;
                    }
                }
                return false;
            }
            else
            {
                Guid uuid = Guid.Parse(id);
                return HasId(uuid);
            }
        }

        public object LookupOriginal(Guid id)
        {
            if (_uuidToOriginal.ContainsKey(id))
            {
                return _uuidToOriginal[id];
            }
            return null;
        }
        public object LookupOriginal(string id)
        {
            if (id.Contains("/"))
            {
                var parentIds = id.Split('/');
                var guid = Guid.Parse(parentIds[0]);
                if (_subDataSources.ContainsKey(guid))
                {
                    foreach (var data in _subDataSources[guid].Values)
                    {
                        var original = data.LookupOriginal(id.Replace(parentIds[0] + "/", ""));
                        if (original != null)
                            return original;
                    }
                }
                return null;
            }
            else
            {
                Guid uuid = Guid.Parse(id);
                return LookupOriginal(uuid);
            }
        }

        public Guid IdFromOriginal(object item)
        {
            if (_originalToUuid.ContainsKey(item))
            {
                return _originalToUuid[item];
            }
            return Guid.Empty;
        }

        public bool HasOriginal(object item) 
        {
            return _originalToUuid.ContainsKey(item);
        }

        

        public static IJSDataSource Create(Object data, DataSourceManager manager, RuntimeHelper helper) {
            if (data == null) {
                return null;
            }

            if (data.GetType().IsArray) {
                return UnmarshalledDataSource.CreateFromArray((Object[])data, null, manager, helper);
            } else if (data is IList) {
                return UnmarshalledDataSource.CreateFromIList((IList)data, null, manager, helper);
            } else if (data is IEnumerable) {
                return UnmarshalledDataSource.CreateFromIEnumerable((IEnumerable)data, null, manager, helper);
            }
            return null;
        }
        private static IJSDataSource CreateFromIEnumerable(IEnumerable data, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper)
        {
            return CreateFromIEnumerable(data, null, schema, manager, helper);
        }
        private static IJSDataSource CreateFromIEnumerable(IEnumerable data, string parentId, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper) 
        {
            UnmarshalledDataSource newData = new UnmarshalledDataSource();
            newData._helper = helper;
            newData._manager = manager;
            newData._parentId = parentId;
            newData._parentSchema = schema;
            bool found = false;
            foreach (var item in data) {
                found = true;
                newData.Add(item);
            }
            if (!found) 
            {
                EnsureParentSchema(newData);
            }
            newData.Listen(data);
            return newData;
        }

        private static IJSDataSource CreateFromIList(IList data, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper)
        {
            return CreateFromIList(data, null, schema, manager, helper);
        }
        private static IJSDataSource CreateFromIList(IList data, string parentId, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper) 
        {
            //Console.WriteLine("test json");
            //DateTime testTime = DateTime.Now;
            //var ser = System.Text.Json.JsonSerializer.Serialize(data);
            
            //Console.WriteLine("end test json:" + (DateTime.Now - testTime).TotalMilliseconds);
            //Console.WriteLine("begin unmarshalled create from list");
            DateTime startTime = DateTime.Now;
            UnmarshalledDataSource newData = new UnmarshalledDataSource();
            newData.Capacity = data.Count;
            newData._helper = helper;
            newData._manager = manager;
            newData._parentId = parentId;
            newData._parentSchema = schema;
            var count = data.Count;
            for (int i = 0; i < count; i++) {
                newData.Add(data[i]);
            }
            if (count == 0) {
                EnsureParentSchema(newData);
            }
            newData.Listen(data);
            //Console.WriteLine("end unmarshalled create from list: " + (DateTime.Now - startTime).TotalMilliseconds);
            return newData;
        }

        private static void EnsureParentSchema(UnmarshalledDataSource data)
        {
            if (data._parentSchema != null) 
            {
                if (data._parentSchema.ItemSchema != null) {
                    data._schema = data._parentSchema.ItemSchema;

                    if (data._schema != null && data._columns == null)
                    {
                        data._columns = data.AdjustCapacity("", data._columns, data._schema, data.Capacity, data.Capacity);
                    }
                }
            }
        }

        private static IJSDataSource CreateFromArray(Array data, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper)
        {
            return CreateFromArray(data, null, schema, manager, helper);
        }
        private static IJSDataSource CreateFromArray(Array data, string parentId, JSDataSourceSchema schema, DataSourceManager manager, RuntimeHelper helper) 
        {
            UnmarshalledDataSource newData = new UnmarshalledDataSource();
            newData._helper = helper;
            newData._parentSchema = schema;
            newData._manager = manager;
            newData._parentId = parentId;
            for (int i = 0; i < data.Length; i++) {
                //newData.Add(data[i]);
                newData.Add(data.GetValue(i));
            }
            if (data.Length == 0) {
                EnsureParentSchema(newData);
            }
            newData.Listen(data);
            return newData;
        }

        private JSDataSourceSchema _schema = null;

        private int _leadingNullItems = 0; 

        private void Add(object item) {
            if (_schema == null)
            {
                EnsureSchema(item);
            }
            if (_schema == null && item == null)
            {
                _leadingNullItems++;
                return;
            }
            EnsureCapacity(_size + 1);
            InsertItemAt(item, _size, _schema, _columns);
            //IJSDataSourceItem itemJson = UnmarshalledDataSourceItem.Create(item, _schema, _columns, _manager);
            //OnAddItem(itemJson, item);
            //_data.Add(itemJson);
        }

        private void EnsureLeadingNullsInserted(JSDataSourceSchema schema, UnmarshalledColumnData[] columns)
        {
            if (_leadingNullItems > 0)
            {
                //Console.WriteLine("dealing with leading nulls.");
                var toInsert = _leadingNullItems;
                _leadingNullItems = 0;
                int j = 0;
                while (toInsert > 0)
                {
                    InsertItemAt(null, j++, schema, columns);
                    toInsert--;
                }
            }
        }

        private void InsertItemAt(object item, int index, JSDataSourceSchema schema, UnmarshalledColumnData[] columns)
        {
            EnsureLeadingNullsInserted(schema, columns);
            EnsureCapacity(_size + 1);
            for (var i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                if (column == null)
                {
                    //Console.WriteLine("column null at: " + i);
                    continue;
                }
                //Console.WriteLine(column.PropertyName);
                column.Insert(_size, column, index, item);              
            }

             _size++;
        }

        private void UpdateItemAt(object oldItem, object newItem, int index, JSDataSourceSchema schema, UnmarshalledColumnData[] columns)
        {
            EnsureLeadingNullsInserted(schema, columns);
            for (var i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                
                column.Update(_size, column, index, oldItem, newItem);              
            }
        }

        private void RemoveItemAt(int index, JSDataSourceSchema schema, UnmarshalledColumnData[] columns)
        {
            EnsureLeadingNullsInserted(schema, columns);
            for (var i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                
                column.Remove(_size, column, index);              
            }
            _size--;
        }

        // private void OnAddItem(IJSDataSourceItem itemJson, Object item) {
        //     _uuidToItem[itemJson.Id] = itemJson;
        //     if (item != null) {
        //         _originalToItem[item] = itemJson;
        //         _itemToOriginal[itemJson] = item;
        //     }
        // }
        // private void OnRemove(IJSDataSourceItem itemJson, object item) 
        // {
        //     if (_uuidToItem.ContainsKey(itemJson.Id)) {
        //         _uuidToItem.Remove(itemJson.Id);
        //     }
        //     if (_itemToOriginal.ContainsKey(itemJson)) {
        //         Object original = _itemToOriginal[itemJson];
        //         if (_originalToItem.ContainsKey(original)) {
        //             _originalToItem.Remove(original);
        //         }
        //         _itemToOriginal.Remove(itemJson);
        //     }
        //     if (item != null &&
        //         _originalToItem.ContainsKey(item)) {
        //         _itemToOriginal.Remove((IJSDataSourceItem)item);
        //     }
        // }

        public static JSDataSourceSchema ExtractSchema(object item) {
            if (item == null) {
                return  null;
            }

            Type c = item.GetType();
            if (c.IsArray) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                var isEmpty = item != null && ((Array)item).Length == 0;
                if (isEmpty)
                {
                    var eleType = c.GetElementType();
                    s.ItemSchema = ExtractSchemaFromType(eleType);
                }
                s.Commit();
                return s;
            }
            else if (item is IList) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                var isEmpty = item != null && ((IList)item).Count == 0;
                if (isEmpty && GetIListTypeArg(item.GetType()) != null)
                {
                    var eleType = GetIListTypeArg(item.GetType());
                    s.ItemSchema = ExtractSchemaFromType(eleType);
                }
                s.Commit();
                return s;
            }
            else if (item is Dictionary<string, object>)
            {
                return JSDataSourceSchema.CreateFromDictionary((IDictionary)item);
            }
            else if (item is IEnumerable && !(item is string)) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                bool isEmpty = true;
                foreach (var subI in (IEnumerable)item)
                {
                    isEmpty = false;
                }
                if (isEmpty && GetIEnumerableTypeArg(item.GetType()) != null)
                {
                    var eleType = GetIEnumerableTypeArg(item.GetType());
                    s.ItemSchema = ExtractSchemaFromType(eleType);
                }
                s.Commit();
                return s;
            }
            
            if (IsPrimitive(c))
            {
                var p = new JSDataSourceSchema();
                p.IsPrimitive = true;
                p.PrimitiveType = p.ResolveSchemaType(c);
                p.Commit();
                return p;
            }
            return JSDataSourceSchema.Create(c);
        }

        private static Type GetIListTypeArg(Type itemType)
        {
            foreach (var inter in itemType.GetInterfaces())
            {
                if (!inter.IsGenericType)
                {
                    continue;
                }
                if (inter.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    return inter.GetGenericArguments()[0];
                }
            }
            return null;
        }

        private static Type GetIEnumerableTypeArg(Type itemType)
        {
            foreach (var inter in itemType.GetInterfaces())
            {
                if (!inter.IsGenericType)
                {
                    continue;
                }
                if (inter.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return inter.GetGenericArguments()[0];
                }
            }
            return null;
        }

        public static JSDataSourceSchema ExtractSchemaFromType(Type itemType) {
            if (itemType.IsArray) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                //if (isEmpty)
                {
                    var eleType = itemType.GetElementType();
                    s.ItemSchema = ExtractSchemaFromType(eleType);
                }
                s.Commit();
                return s;
            }
            else if (typeof(IList).IsAssignableFrom(itemType)) 
            {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                if (GetIListTypeArg(itemType) != null)
                {
                    var eleType = GetIListTypeArg(itemType);
                    s.ItemSchema = ExtractSchemaFromType(eleType);
                }
                s.Commit();
                return s;
            }
            else if (typeof(IEnumerable).IsAssignableFrom(itemType) &&
            !(typeof(string) == itemType)) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                if (GetIEnumerableTypeArg(itemType) != null)
                {
                    var eleType = GetIEnumerableTypeArg(itemType);
                    s.ItemSchema = ExtractSchemaFromType(eleType);
                }
                s.Commit();
                return s;
            }

            if (IsPrimitive(itemType))
            {
                var p = new JSDataSourceSchema();
                p.IsPrimitive = true;
                p.Commit();
                return p;
            }
            return JSDataSourceSchema.Create(itemType);
        }

        private static bool IsPrimitive(Type type)
        {
            if (type.IsPrimitive || type == typeof(string))
            {
                return true;
            }
            return false;
        }

        public string GetDataIntentsAsJson()
        {
            if (_schema != null)
            {
                return _schema.GetDataIntentsAsJson();
            }
            return null;
        }

        private void EnsureSchema(object item) 
        {          
            if (item != null && _schema == null) {
                //Console.WriteLine("begin ensure schema");
                DateTime startTime = DateTime.Now;

                if (_parentSchema != null) {
                    if (_parentSchema.ItemSchema != null) {
                        _schema = _parentSchema.ItemSchema;
                    }
                }
                _schema = ExtractSchema(item);
                if (_schema != null && _columns == null)
                {
                    _columns = AdjustCapacity("", _columns, _schema, Capacity, Capacity);
                }
                if (_parentSchema != null && _parentSchema.ItemSchema == null) {
                    //Console.WriteLine("set item schema");
                    _parentSchema.ItemSchema = _schema;
                }
                //Console.WriteLine("end ensure schema: " + (DateTime.Now - startTime).TotalMilliseconds);
            }
        }

        public IJSDataSourceItem NotifyInsertItem(object data, int index, Object item) {
            EnsureSchema(item);
            if (_schema == null && item == null)
            {
                _leadingNullItems++;
                return null;
            }
            // IJSDataSourceItem itemJson = UnmarshalledDataSourceItem.Create(item, _schema, _manager);
            // OnAddItem(itemJson, item);
            // _data.Insert(index, itemJson);
            EnsureCapacity(_size + 1);
            InsertItemAt(item, index, _schema, _columns);

            return null;
        }

        public IJSDataSourceItem NotifyRemoveItem(object data, int index, object oldItem) {
            EnsureSchema(oldItem);
            if (_schema == null)
            {
                _leadingNullItems--;
                return null;
            }

            RemoveItemAt(index, _schema, _columns);

            return null;
            // IJSDataSourceItem itemJson = _data[index];
            // OnRemove(itemJson, oldItem);
            // _data.RemoveAt(index);
            // return itemJson;
        }

        

        public void NotifyClearItems(Object data) {
            if (_size > 0)
            {
                if (_columns != null)
                {
                    for (var i = 0; i < _columns.Length; i++)
                    {
                        _columns[i].Clear(_size, _columns[i]);
                    }
                }
            }
            _size = 0;
            _schema = null;
            _columns = null;
            if (data.GetType().IsArray) {
                object[] dataArr = (object[])data;
                for (int i = 0; i < dataArr.Length; i++) {
                    Add(dataArr[i]);
                }
            } else if (data is IList) {
                IList dataList = (IList)data;
                for (int i = 0; i < dataList.Count; i++) {
                    Add(dataList[i]);
                }
            } else if (data is IEnumerable) {
                IEnumerable dataIter = (IEnumerable)data;
                foreach (object item in dataIter) {
                    Add(item);
                }
            }
        }

        public IJSDataSourceItem NotifySetItem(Object data, int index, Object oldItem, Object newItem) 
        {
            EnsureSchema(newItem);
            if (_schema == null)
            {
                return null;
            }
            
            UpdateItemAt(oldItem, newItem, index, _schema, _columns);

            return null;
        }
        
        public IJSDataSourceItem NotifyUpdateItem(object data, int index, object item) 
        {
            EnsureSchema(item);
            if (_schema == null)
            {
                return null;
            }
            
            UpdateItemAt(item, item, index, _schema, _columns);

            return null;
        }

        public void InsertItemWithId(string id, int index, Object item)
        {
            EnsureSchema(item);
            if (_schema == null && item == null)
            {
                _leadingNullItems++;
                return;
            }
            var guid = Guid.Parse(id);

            if (_uuidToOriginal.ContainsKey(guid))
                return;

            if (_originalToUuid.ContainsKey(item))
                return;

            _uuidToOriginal.Add(guid, item);
            _originalToUuid.Add(item, guid);

            EnsureCapacity(_size + 1);
            InsertItemAt(item, index, _schema, _columns);
        }
    }

}