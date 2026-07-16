using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace IgniteUI.Blazor.Controls
{
internal class CollectionAdapter<T, J> 
    where T: class 
    where J: class {
    private IList<T> _queryItems;
    private IList<T> _manualItems = new List<T>();

    private IList<T> _allList;
    private IList<J> _target;
    private IList<T> _query;
    private Func<T, J> _toTarget;
    private Action<T> _onItemAdded;
    private Action<T> _onItemRemoved;

    

    private bool _hasShiftedOnceAlready;

    public CollectionAdapter(IList<T> query, IList<J> target, IList<T> allList, Func<T, J> toTarget, Action<T> onItemAdded, Action<T> onItemRemoved, Func<T, string> collisionChecker = null) {
        if (collisionChecker != null) {
            this._collisionChecker = collisionChecker;
        }
        this._query = query;
        this._target = target;
        this._toTarget = toTarget;
        this._allList = allList;

        this._onItemAdded = onItemAdded;
        this._onItemRemoved = onItemRemoved;

        this.SyncItems();

        if (this._query is INotifyCollectionChanged) {
            ((INotifyCollectionChanged)this._query).CollectionChanged += (s, o) => this.OnQueryChanged();
        }
    }

    private Func<T, string> _collisionChecker = null;

    public Func <T, string> CollisionChecker {
        get {
            return this._collisionChecker;
        }
        set {
            this._collisionChecker = value;
        }
    }
    
    // private void UpdateQuery(q: any) {
    //     this._query = q;
    //     if (this._query.changes) {
    //         this._query.changes.subscribe((v) => this.onQueryChanged(v));
    //     }
    //     this.notifyContentChanged();
    // }

    public void SubcribeToManual(INotifyCollectionChanged manual)
    {
        manual.CollectionChanged += this.OnManualChanged;
    }
    public void UnsubcribeToManual(INotifyCollectionChanged manual)
    {
        manual.CollectionChanged -= this.OnManualChanged;
    }

    public void UpdateAllList(IList<T> allList)
    {
        _allList = allList;
    }
    public void UpdateTarget(IList<J> target)
    {
        _target = target;
    }

    private void OnManualChanged(object sender, NotifyCollectionChangedEventArgs args)
    {
        switch (args.Action) {
            case NotifyCollectionChangedAction.Add:
                this.InsertManualItem(args.NewStartingIndex, (T)args.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Remove:
                this.RemoveManualItemAt(args.OldStartingIndex);
                break;
            case NotifyCollectionChangedAction.Replace:
                this.RemoveManualItemAt(args.OldStartingIndex);
                this.InsertManualItem(args.NewStartingIndex, (T)args.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Reset:
                this.ClearManualItems();
                break;
        }
    }


    public void ShiftContentToManual(IList<T> manualCollection, Action<T> onMoving)
    {       
        T item = default(T);

        var manualSet = new HashSet<string>();
        if (this.CollisionChecker != null) {
            for (var i = 0; i < this._manualItems.Count; i++) {
                item = this._manualItems[i];
                if (item != null) {
                    var key = this.CollisionChecker(item);
                    if (key != null) {
                        if (!manualSet.Contains(key)) {
                            manualSet.Add(key);
                        }
                    }
                }
            }
        }

        var mapWasEmpty = manualSet.Count == 0;
        for (var i = 0; i < this._query.Count; i++) {
            item = this._query[i];

            if (!this._hasShiftedOnceAlready) {
                this._manualItems.Insert(i, item);
                manualCollection.Insert(i, item);
                onMoving(item);
            } else {
                var key = this.CollisionChecker(item);
                if (key == null) {
                    this._manualItems.Insert(i, item);
                    manualCollection.Insert(i, item);
                    onMoving(item);
                } else {
                    if (!manualSet.Contains(key)) {
                        this._manualItems.Insert(manualCollection.Count, item);
                        manualCollection.Insert(manualCollection.Count, item);
                        onMoving(item);
                    }
                }
            }
        }
        this.SyncItems();

        this._hasShiftedOnceAlready = true;
    }

    private List<T> actualContent = new List<T>();

    private void SyncItems() {
        Dictionary<T, bool> targetMap = new Dictionary<T, bool>();
        Dictionary<T, bool> queryMap = new Dictionary<T, bool>();
        Dictionary<T, bool> manualMap = new Dictionary<T, bool>();

        T item = default(T);
        for (var i = 0; i < this._allList.Count; i++) {
            item = this._allList[i];
            targetMap[item] = true;
        }
       
        var queryArray = new List<T>(this._query);
        this.actualContent = queryArray;

        if (this.CollisionChecker != null) {
            
            var manualKeySet = new HashSet<string>();
            for (var i = 0; i < this._manualItems.Count; i++) {
                item = this._manualItems[i];
                if (item != null) {
                    var key = this.CollisionChecker(item);
                    if (key != null) {
                        if (!manualKeySet.Contains(key)) {
                            manualKeySet.Add(key);
                        }
                    }
                }
            }
            for (var i = this._query.Count - 1; i >= 0; i--) {
                item = queryArray[i];
                if (item == null) {
                    queryArray.RemoveAt(i);
                }
                else {
                    var key = this.CollisionChecker(item);
                    if (key != null && (manualKeySet.Contains(key) || this._removedManualKeys.Contains(key))) {
                        queryArray.RemoveAt(i);
                    }
                }
            }
        }


        for (var i = 0; i < queryArray.Count; i++) {
            item = queryArray[i];
            queryMap[item] = true;
        }
        for (var i = 0; i < this._manualItems.Count; i++) {
            item = this._manualItems[i];
            manualMap[item] = true;
        }

        for (var i = this._allList.Count - 1; i >= 0; i--) {
            item = this._allList[i];
            if (!queryMap.ContainsKey(item) && !manualMap.ContainsKey(item)) {
                this._allList.RemoveAt(i);
                this._target.RemoveAt(i);
                this._onItemRemoved(item);
            }
        }

        int ind = 0;
        int ins = 0;
        T insItem = default(T);
        int maxLen = queryArray.Count + this._manualItems.Count;
        while (ind < maxLen) {
            if (ind < queryArray.Count) {
                insItem = queryArray[ind];
            } else if ((ind - queryArray.Count) < this._manualItems.Count) {
                insItem = this._manualItems[ind - queryArray.Count];
            } else {
                break;
            }
            if (ins < this._allList.Count) {
                item = this._allList[ins];
                if (item == insItem) {
                    ins++;
                    ind++;
                } else {
                    this._allList.Insert(ins, insItem);
                    this._target.Insert(ins, this._toTarget(insItem));
                    this._onItemAdded(insItem);
                    ind++;
                    ins++;
                }
            } else {
                this._allList.Add(insItem);
                this._target.Add(this._toTarget(insItem));
                this._onItemAdded(insItem);
                ind++;
                ins++;
            }
        }
    }

    public void NotifyContentChanged() {
        this.OnQueryChanged();
    }

    private void OnQueryChanged() {
        this.SyncItems();
    }

    public void AddManualItem(T item) {
        if (this.CollisionChecker != null) {
            var key = this.CollisionChecker(item);
            if (key != null) {
                if (this._removedManualKeys.Contains(key)) {
                    this._removedManualKeys.Remove(key);
                }
            }
        }
        this._manualItems.Add(item);
        this.SyncItems();
    }

    private HashSet<string> _removedManualKeys = new HashSet<string>();
    public bool RemoveManualItem(T item)
    {
        var ind = this._manualItems.IndexOf(item);
        if (ind >= 0) {
            if (this.CollisionChecker != null) {
                var key = this.CollisionChecker(item);
                if (key != null) {
                    if (!this._removedManualKeys.Contains(key)) {
                        this._removedManualKeys.Add(key);
                    }
                }
            }

            this._manualItems.RemoveAt(ind);
            this.SyncItems();

            return true;
        }
        return false;
    }

    public void RemoveManualItemAt(int index) {
        if (index < this._manualItems.Count) {
            var item = this._manualItems[index];
            if (this.CollisionChecker != null) {
                var key = this.CollisionChecker(item);
                if (key != null) {
                    if (!this._removedManualKeys.Contains(key)) {
                        this._removedManualKeys.Add(key);
                    }
                }
            }
        }
        this._manualItems.RemoveAt(index);
        this.SyncItems();
    }

    public void ClearManualItems() {
        if (this.CollisionChecker != null) {
            this._removedManualKeys.Clear();
        }
        this._manualItems.Clear();
        this.SyncItems();
    }

    public void InsertManualItem(int index, T item) {
        if (this.CollisionChecker != null) {
            var key = this.CollisionChecker(item);
            if (key != null) {
                if (this._removedManualKeys.Contains(key)) {
                    this._removedManualKeys.Remove(key);
                }
            }
        }
        this._manualItems.Insert(index, item);
        this.SyncItems();
    }
}

}