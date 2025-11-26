import { Base, typeCast, String_$type, Type, markType } from "./type";
import { DiffApplyInfo } from "./DiffApplyInfo";
import { DescriptionTreeNode } from "./DescriptionTreeNode";
import { List$1 } from "./List$1";
import { DescriptionTreeAction } from "./DescriptionTreeAction";
import { DescriptionTreeActionType } from "./DescriptionTreeActionType";
import { HashSet$1 } from "./HashSet$1";
import { DescriptionPropertyValue } from "./DescriptionPropertyValue";
import { Dictionary$2 } from "./Dictionary$2";
import { DescriptionTreeReconciler_TreeModeData } from "./DescriptionTreeReconciler_TreeModeData";
import { TypeDescriptionMetadata } from "./TypeDescriptionMetadata";
import { TypeDescriptionWellKnownType } from "./TypeDescriptionWellKnownType";

/**
 * @hidden 
 */
export class DescriptionTreeReconciler extends Base {
	static $t: Type = markType(DescriptionTreeReconciler, 'DescriptionTreeReconciler');
	static applyDiff(oldTree: DescriptionTreeNode, diff: List$1<DescriptionTreeAction>): DiffApplyInfo {
		let info = new DiffApplyInfo();
		for (let i = 0; i < diff.count; i++) {
			let currAction = diff._inner[i];
			switch (currAction.actionType) {
				case DescriptionTreeActionType.UpdateProperty:
				if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, currAction.oldValue) !== null) {
					let id = (<DescriptionTreeNode>currAction.oldValue).id;
					info.removedIds.add_1(id);
				}
				currAction.targetNode.updateWithMeta(currAction.propertyName, currAction.newValue, currAction.propertyMetadata);
				if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, currAction.newValue) !== null) {
					let id1 = (<DescriptionTreeNode>currAction.newValue).id;
					if (info.removedIds.contains(id1)) {
						info.removedIds.remove(id1);
					}
					info.newPropertyValues.add(currAction);
				}
				break;

				case DescriptionTreeActionType.ResetProperty:
				if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, currAction.oldValue) !== null) {
					let id2 = (<DescriptionTreeNode>currAction.oldValue).id;
					info.removedIds.add_1(id2);
				}
				currAction.targetNode.remove(currAction.propertyName);
				break;

				case DescriptionTreeActionType.ReplaceItem:
				{
					let arr = <any[]>currAction.targetNode.get(currAction.propertyName).value;
					let oldItem = arr[currAction.oldIndex];
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, oldItem) !== null) {
						let id3 = (<DescriptionTreeNode>oldItem).id;
						info.removedIds.add_1(id3);
					}
					arr[currAction.oldIndex] = currAction.newValue;
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, currAction.newValue) !== null) {
						let id4 = (<DescriptionTreeNode>currAction.newValue).id;
						if (info.removedIds.contains(id4)) {
							info.removedIds.remove(id4);
						}
						info.newCollectionContent.add(currAction);
					}
				}
				break;

				case DescriptionTreeActionType.RemoveItem:
				{
					let arr1 = <any[]>currAction.targetNode.get(currAction.propertyName).value;
					let oldItem1 = arr1[currAction.oldIndex];
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, oldItem1) !== null) {
						let id5 = (<DescriptionTreeNode>oldItem1).id;
						info.removedIds.add_1(id5);
					}
					let newArr: any[] = <any[]>new Array(arr1.length - 1);
					let j: number = 0;
					for (let k = 0; k < arr1.length; k++) {
						if (k != currAction.oldIndex) {
							newArr[j] = arr1[k];
							j++;
						}
					}
					currAction.targetNode.update(currAction.propertyName, newArr);
				}
				break;

				case DescriptionTreeActionType.InsertItem:
				{
					if (currAction.targetNode == null && oldTree == null) {
						info.newRootContent.add(currAction);
					} else {
						let arr2 = <any[]>currAction.targetNode.get(currAction.propertyName).value;
						let newArr1: any[] = <any[]>new Array(arr2.length + 1);
						let j1: number = 0;
						for (let k1 = 0; k1 < newArr1.length; k1++) {
							if (k1 == currAction.newIndex) {
								newArr1[k1] = currAction.newValue;
							} else {
								newArr1[k1] = arr2[j1];
								j1++;
							}
						}
						currAction.targetNode.update(currAction.propertyName, newArr1);
						if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, currAction.newValue) !== null) {
							let id6 = (<DescriptionTreeNode>currAction.newValue).id;
							if (info.removedIds.contains(id6)) {
								info.removedIds.remove(id6);
							}
							info.newCollectionContent.add(currAction);
						}
					}
				}
				break;

				case DescriptionTreeActionType.ClearItems:
				{
					let arr3 = <any[]>currAction.targetNode.get(currAction.propertyName).value;
					for (let k2 = 0; k2 < arr3.length; k2++) {
						let oldItem2 = arr3[k2];
						if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, oldItem2) !== null) {
							let id7 = (<DescriptionTreeNode>oldItem2).id;
							info.removedIds.add_1(id7);
						}
					}
					currAction.targetNode.update(currAction.propertyName, <any[]>[]);
				}
				break;

			}

		}
		return info;
	}
	static diffTrees(oldTree: DescriptionTreeNode, newTree: DescriptionTreeNode, isDelta: boolean): List$1<DescriptionTreeAction> {
		let ret = new List$1<DescriptionTreeAction>((<any>DescriptionTreeAction).$type, 0);
		if (oldTree == null && newTree == null) {
			return ret;
		}
		if (oldTree != null && newTree == null) {
			let act: DescriptionTreeAction = new DescriptionTreeAction();
			act.actionType = DescriptionTreeActionType.RemoveItem;
			act.oldIndex = 0;
			act.oldValue = oldTree;
			act.propertyMetadata = null;
			act.propertyName = null;
			ret.add(act);
			return ret;
		}
		if (oldTree == null && newTree != null) {
			let act1: DescriptionTreeAction = new DescriptionTreeAction();
			act1.actionType = DescriptionTreeActionType.InsertItem;
			act1.oldIndex = 0;
			act1.newValue = newTree;
			act1.propertyMetadata = null;
			act1.propertyName = null;
			ret.add(act1);
			return ret;
		}
		if (newTree.type != oldTree.type) {
			let act2: DescriptionTreeAction = new DescriptionTreeAction();
			act2.actionType = DescriptionTreeActionType.ReplaceItem;
			act2.oldIndex = 0;
			act2.oldValue = oldTree;
			act2.newValue = newTree;
			act2.propertyMetadata = null;
			act2.propertyName = null;
			ret.add(act2);
			return ret;
		}
		DescriptionTreeReconciler.diffTreesHelper(ret, oldTree, newTree, isDelta);
		return ret;
	}
	private static diffTreesHelper(actions: List$1<DescriptionTreeAction>, oldTree: DescriptionTreeNode, newTree: DescriptionTreeNode, isDelta: boolean): void {
		newTree.id = oldTree.id;
		let oldNonArrayProperties = DescriptionTreeReconciler.getNonArrayProperties(oldTree);
		let newNonArrayProperties = DescriptionTreeReconciler.getNonArrayProperties(newTree);
		let staleValues = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		let newValues = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		let updatedValues = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		for (let i = 0; i < oldNonArrayProperties.count; i++) {
			let curr = oldNonArrayProperties._inner[i];
			if (newTree.has(curr.propertyName)) {
				if (!Base.equalsStatic(curr, newTree.get(curr.propertyName))) {
					let newProp = newTree.get(curr.propertyName);
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, curr.value) !== null && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newProp.value) !== null) {
						let oldTreeNode = <DescriptionTreeNode>curr.value;
						let newTreeNode = <DescriptionTreeNode>newProp.value;
						if (oldTreeNode.type == newTreeNode.type) {
							if ((!oldTreeNode.has("Name") && !newTreeNode.has("Name")) || (oldTreeNode.has("Name") && newTreeNode.has("Name") && (oldTreeNode.get("Name").value == newTreeNode.get("Name").value))) {
								DescriptionTreeReconciler.diffTreesHelper(actions, oldTreeNode, newTreeNode, isDelta);
							} else {
								updatedValues.add(newTree.get(curr.propertyName));
							}
						} else {
							updatedValues.add(newTree.get(curr.propertyName));
						}
					} else {
						updatedValues.add(newTree.get(curr.propertyName));
					}
				}
			} else {
				if (!isDelta) {
					staleValues.add(curr);
				}
			}
		}
		for (let i1 = 0; i1 < newNonArrayProperties.count; i1++) {
			let curr1 = newNonArrayProperties._inner[i1];
			if (!oldTree.has(curr1.propertyName)) {
				newValues.add(curr1);
			}
		}
		for (let i2 = 0; i2 < staleValues.count; i2++) {
			let curr2 = staleValues._inner[i2];
			let act: DescriptionTreeAction = new DescriptionTreeAction();
			act.targetNode = oldTree;
			act.currentNode = newTree;
			act.actionType = DescriptionTreeActionType.ResetProperty;
			act.propertyName = curr2.propertyName;
			act.propertyMetadata = curr2.metadata;
			act.oldValue = curr2.value;
			actions.add(act);
		}
		for (let i3 = 0; i3 < newValues.count; i3++) {
			let curr3 = newValues._inner[i3];
			let act1: DescriptionTreeAction = new DescriptionTreeAction();
			act1.targetNode = oldTree;
			act1.currentNode = newTree;
			act1.actionType = DescriptionTreeActionType.UpdateProperty;
			act1.propertyName = curr3.propertyName;
			act1.propertyMetadata = curr3.metadata;
			act1.newValue = curr3.value;
			act1.oldValue = null;
			actions.add(act1);
		}
		for (let i4 = 0; i4 < updatedValues.count; i4++) {
			let curr4 = updatedValues._inner[i4];
			let oldVal = oldTree.get(curr4.propertyName).value;
			let newVal = curr4.value;
			if (!Base.equalsStatic(newVal, oldVal)) {
				let act2: DescriptionTreeAction = new DescriptionTreeAction();
				act2.targetNode = oldTree;
				act2.currentNode = newTree;
				act2.actionType = DescriptionTreeActionType.UpdateProperty;
				act2.propertyName = curr4.propertyName;
				act2.propertyMetadata = curr4.metadata;
				act2.newValue = curr4.value;
				act2.oldValue = oldTree.get(curr4.propertyName).value;
				actions.add(act2);
			}
		}
		let oldArrayProperties = DescriptionTreeReconciler.getArrayProperties(oldTree);
		let newArrayProperties = DescriptionTreeReconciler.getArrayProperties(newTree);
		let staleArrayValues = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		let newArrayValues = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		let updatedArrayValues = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		for (let i5 = 0; i5 < oldArrayProperties.count; i5++) {
			let curr5 = oldArrayProperties._inner[i5];
			if (newTree.has(curr5.propertyName)) {
				if (!DescriptionTreeReconciler.arraysEquivalent(curr5.value, newTree.get(curr5.propertyName).value)) {
					updatedArrayValues.add(newTree.get(curr5.propertyName));
				} else {
					let oldArr = <any[]>curr5.value;
					let newArr = <any[]>newTree.get(curr5.propertyName).value;
					if (oldArr != null) {
						for (let j = 0; j < oldArr.length; j++) {
							let oldItem = oldArr[j];
							let newItem = newArr[j];
							if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, oldItem) !== null && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newItem) !== null) {
								let oldTreeNode1 = <DescriptionTreeNode>oldItem;
								let newTreeNode1 = <DescriptionTreeNode>newItem;
								let isDiffRef: boolean = false;
								let isReference: boolean = oldTreeNode1.type == newTreeNode1.type && oldTreeNode1.type == "EmbeddedRef";
								if (isReference) {
									let oldTreeRefItems = oldTreeNode1.items().toArray();
									let newTreeRefItems = newTreeNode1.items().toArray();
									for (let k: number = 0; k <= oldTreeRefItems.length - 1; k++) {
										let oldItemRef = oldTreeRefItems[k];
										let newItemRef = newTreeRefItems[k];
										if (oldItemRef.propertyName != newItemRef.propertyName || oldItemRef.value != newItemRef.value) {
											isDiffRef = true;
											break;
										}
									}
								}
								if (oldTreeNode1.type == newTreeNode1.type && !isDiffRef) {
									DescriptionTreeReconciler.diffTreesHelper(actions, oldTreeNode1, newTreeNode1, isDelta);
								} else {
									let act3 = new DescriptionTreeAction();
									act3.propertyName = curr5.propertyName;
									act3.propertyMetadata = curr5.metadata;
									act3.targetNode = oldTree;
									act3.currentNode = newTree;
									act3.actionType = DescriptionTreeActionType.ReplaceItem;
									act3.oldIndex = j;
									act3.newIndex = j;
									act3.oldValue = oldTreeNode1;
									act3.newValue = newTreeNode1;
									actions.add(act3);
								}
							} else {
								if (!Base.equalsStatic(oldItem, newItem)) {
									let act4 = new DescriptionTreeAction();
									act4.propertyName = curr5.propertyName;
									act4.propertyMetadata = curr5.metadata;
									act4.targetNode = oldTree;
									act4.currentNode = newTree;
									act4.actionType = DescriptionTreeActionType.ReplaceItem;
									act4.oldIndex = j;
									act4.newIndex = j;
									act4.oldValue = oldItem;
									act4.newValue = newItem;
									actions.add(act4);
								}
							}
						}
					}
				}
			} else {
				if (!isDelta) {
					staleArrayValues.add(curr5);
				}
			}
		}
		for (let i6 = 0; i6 < newArrayProperties.count; i6++) {
			let curr6 = newArrayProperties._inner[i6];
			if (!oldTree.has(curr6.propertyName)) {
				newArrayValues.add(curr6);
			}
		}
		for (let i7 = 0; i7 < staleArrayValues.count; i7++) {
			let curr7 = staleArrayValues._inner[i7];
			let act5 = new DescriptionTreeAction();
			act5.targetNode = oldTree;
			act5.currentNode = newTree;
			act5.propertyName = curr7.propertyName;
			act5.propertyMetadata = curr7.metadata;
			act5.actionType = DescriptionTreeActionType.ClearItems;
			act5.oldValue = curr7.value;
			actions.add(act5);
		}
		for (let i8 = 0; i8 < newArrayValues.count; i8++) {
			let curr8 = newArrayValues._inner[i8];
			let act6 = new DescriptionTreeAction();
			act6.targetNode = oldTree;
			act6.currentNode = newTree;
			act6.propertyName = curr8.propertyName;
			act6.propertyMetadata = curr8.metadata;
			act6.actionType = DescriptionTreeActionType.UpdateProperty;
			act6.oldValue = null;
			act6.newValue = curr8.value;
			actions.add(act6);
		}
		for (let i9 = 0; i9 < updatedArrayValues.count; i9++) {
			DescriptionTreeReconciler.reconcileArrays(actions, oldTree, newTree, oldTree.get(updatedArrayValues._inner[i9].propertyName), updatedArrayValues._inner[i9], isDelta);
		}
	}
	private static getNodeType(node: DescriptionTreeNode): string {
		if (node == null) {
			return "UNKOWN";
		}
		if (!node.has("Type")) {
			return "UNKOWN";
		}
		return <string>node.get("Type").value;
	}
	private static reconcileArrays(actions: List$1<DescriptionTreeAction>, oldTree: DescriptionTreeNode, newTree: DescriptionTreeNode, oldArray: DescriptionPropertyValue, newArray: DescriptionPropertyValue, isDelta: boolean): void {
		let nameMode: boolean = false;
		let oldArr = <any[]>oldArray.value;
		let newArr = <any[]>newArray.value;
		if (oldArr.length > 0) {
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, oldArr[0]) !== null) {
				let oldNode = <DescriptionTreeNode>oldArr[0];
				if (oldNode.has("Name") && oldNode.get("Name") != null) {
					nameMode = true;
				}
				if (oldNode.has("RefType") && (<string>oldNode.get("RefType").value) == "uuid") {
					nameMode = true;
				}
			}
		}
		if (newArr.length > 0) {
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newArr[0]) !== null) {
				let newNode = <DescriptionTreeNode>newArr[0];
				if (newNode.has("Name") && newNode.get("Name") != null) {
					nameMode = true;
				}
				if (newNode.has("RefType") && (<string>newNode.get("RefType").value) == "uuid") {
					nameMode = true;
				}
			}
		}
		if (nameMode) {
			let oldNodesMap: Dictionary$2<string, DescriptionTreeReconciler_TreeModeData> = new Dictionary$2<string, DescriptionTreeReconciler_TreeModeData>(String_$type, (<any>DescriptionTreeReconciler_TreeModeData).$type, 0);
			let oldNodesList: List$1<DescriptionTreeReconciler_TreeModeData> = new List$1<DescriptionTreeReconciler_TreeModeData>((<any>DescriptionTreeReconciler_TreeModeData).$type, 0);
			let newNodesMap: Dictionary$2<string, DescriptionTreeReconciler_TreeModeData> = new Dictionary$2<string, DescriptionTreeReconciler_TreeModeData>(String_$type, (<any>DescriptionTreeReconciler_TreeModeData).$type, 0);
			let newNodesList: List$1<DescriptionTreeReconciler_TreeModeData> = new List$1<DescriptionTreeReconciler_TreeModeData>((<any>DescriptionTreeReconciler_TreeModeData).$type, 0);
			let badKeyIncr: number = 0;
			for (let i = 0; i < oldArr.length; i++) {
				let data: DescriptionTreeReconciler_TreeModeData = new DescriptionTreeReconciler_TreeModeData();
				data.node = <DescriptionTreeNode>oldArr[i];
				data.index = i;
				data.name = data.node.has("Name") ? <string>data.node.get("Name").value : data.node.has("RefType") ? <string>data.node.get("Value").value : null;
				if (data.name == null) {
					data.name = "BADKEY_" + badKeyIncr.toString();
					badKeyIncr++;
				}
				oldNodesList.add(data);
				oldNodesMap.addItem(data.name, data);
			}
			for (let i1 = 0; i1 < newArr.length; i1++) {
				let data1: DescriptionTreeReconciler_TreeModeData = new DescriptionTreeReconciler_TreeModeData();
				data1.node = <DescriptionTreeNode>newArr[i1];
				data1.index = i1;
				data1.name = data1.node.has("Name") ? <string>data1.node.get("Name").value : data1.node.has("RefType") ? <string>data1.node.get("Value").value : null;
				if (data1.name == null) {
					data1.name = "BADKEY_" + badKeyIncr.toString();
					badKeyIncr++;
				}
				newNodesList.add(data1);
				newNodesMap.addItem(data1.name, data1);
			}
			for (let i2 = 0; i2 < oldNodesList.count; i2++) {
				let oldNode1 = oldNodesList._inner[i2];
				if (!newNodesMap.containsKey(oldNode1.name) || (newNodesMap.containsKey(oldNode1.name) && DescriptionTreeReconciler.getNodeType(oldNode1.node) != DescriptionTreeReconciler.getNodeType(newNodesMap.item(oldNode1.name).node))) {
					oldNode1.toRemove = true;
				}
			}
			for (let i3 = 0; i3 < newNodesList.count; i3++) {
				let newNode1 = newNodesList._inner[i3];
				if (!oldNodesMap.containsKey(newNode1.name) || (oldNodesMap.containsKey(newNode1.name) && DescriptionTreeReconciler.getNodeType(newNode1.node) != DescriptionTreeReconciler.getNodeType(oldNodesMap.item(newNode1.name).node))) {
					newNode1.toAdd = true;
				} else {
					oldNodesMap.item(newNode1.name).targetIndex = i3;
				}
			}
			for (let i4 = 0; i4 < oldNodesList.count; i4++) {
				let oldNode2 = oldNodesList._inner[i4];
				if (oldNode2.toRemove) {
					let act = new DescriptionTreeAction();
					act.propertyName = newArray.propertyName;
					act.targetNode = oldTree;
					act.currentNode = newTree;
					act.propertyMetadata = newArray.metadata;
					act.oldIndex = oldNode2.index;
					act.actionType = DescriptionTreeActionType.RemoveItem;
					act.oldValue = oldNode2.node;
					actions.add(act);
					for (let j = i4 + 1; j < oldNodesList.count; j++) {
						let subsequentOldNode = oldNodesList._inner[j];
						subsequentOldNode.index--;
					}
				}
			}
			for (let i5 = 0; i5 < newNodesList.count; i5++) {
				let newNode2 = newNodesList._inner[i5];
				if (newNode2.toAdd) {
					let act1 = new DescriptionTreeAction();
					act1.propertyName = newArray.propertyName;
					act1.targetNode = oldTree;
					act1.currentNode = newTree;
					act1.propertyMetadata = newArray.metadata;
					act1.newIndex = i5;
					act1.actionType = DescriptionTreeActionType.InsertItem;
					act1.newValue = newArr[i5];
					actions.add(act1);
					for (let j1 = 0; j1 < oldNodesList.count; j1++) {
						let subsequentOldNode1 = oldNodesList._inner[j1];
						if (!subsequentOldNode1.toRemove && subsequentOldNode1.index >= i5) {
							subsequentOldNode1.index++;
						}
					}
				} else {
					let oldNode3 = oldNodesMap.item(newNode2.name);
					if (oldNode3.index == oldNode3.targetIndex) {
						DescriptionTreeReconciler.diffTreesHelper(actions, oldNode3.node, newNode2.node, isDelta);
						continue;
					}
					let act2 = new DescriptionTreeAction();
					act2.propertyName = newArray.propertyName;
					act2.targetNode = oldTree;
					act2.currentNode = newTree;
					act2.propertyMetadata = newArray.metadata;
					act2.oldIndex = oldNode3.index;
					act2.actionType = DescriptionTreeActionType.RemoveItem;
					act2.oldValue = oldNode3.node;
					actions.add(act2);
					for (let j2 = 0; j2 < oldNodesList.count; j2++) {
						let subsequentOldNode2 = oldNodesList._inner[j2];
						if (!subsequentOldNode2.toRemove && subsequentOldNode2.node != oldNode3.node && subsequentOldNode2.index >= oldNode3.index) {
							subsequentOldNode2.index--;
						}
					}
					act2 = new DescriptionTreeAction();
					act2.propertyName = newArray.propertyName;
					act2.targetNode = oldTree;
					act2.currentNode = newTree;
					act2.propertyMetadata = newArray.metadata;
					act2.newIndex = oldNode3.targetIndex;
					act2.actionType = DescriptionTreeActionType.InsertItem;
					act2.newValue = oldNode3.node;
					actions.add(act2);
					oldNode3.index = oldNode3.targetIndex;
					for (let j3 = 0; j3 < oldNodesList.count; j3++) {
						let subsequentOldNode3 = oldNodesList._inner[j3];
						if (!subsequentOldNode3.toRemove && subsequentOldNode3.node != oldNode3.node && subsequentOldNode3.index >= oldNode3.index) {
							subsequentOldNode3.index++;
						}
					}
					DescriptionTreeReconciler.diffTreesHelper(actions, oldNode3.node, newNode2.node, isDelta);
				}
			}
		} else {
			let maxLen = Math.max(oldArr.length, newArr.length);
			for (let i6 = 0; i6 < maxLen; i6++) {
				if (i6 >= oldArr.length) {
					let act3 = new DescriptionTreeAction();
					act3.propertyName = newArray.propertyName;
					act3.targetNode = oldTree;
					act3.currentNode = newTree;
					act3.propertyMetadata = newArray.metadata;
					act3.newIndex = i6;
					act3.actionType = DescriptionTreeActionType.InsertItem;
					act3.newValue = newArr[i6];
					actions.add(act3);
				} else if (i6 >= newArr.length) {
					let act4 = new DescriptionTreeAction();
					act4.propertyName = newArray.propertyName;
					act4.targetNode = oldTree;
					act4.currentNode = newTree;
					act4.propertyMetadata = newArray.metadata;
					act4.oldIndex = newArr.length;
					act4.actionType = DescriptionTreeActionType.RemoveItem;
					act4.oldValue = oldArr[i6];
					actions.add(act4);
				} else {
					let oldItem = oldArr[i6];
					let newItem = newArr[i6];
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, oldItem) !== null && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newItem) !== null) {
						let oldTreeNode = <DescriptionTreeNode>oldItem;
						let newTreeNode = <DescriptionTreeNode>newItem;
						if (oldTreeNode.type == newTreeNode.type) {
							DescriptionTreeReconciler.diffTreesHelper(actions, oldTreeNode, newTreeNode, isDelta);
						} else {
							let act5 = new DescriptionTreeAction();
							act5.propertyName = newArray.propertyName;
							act5.targetNode = oldTree;
							act5.currentNode = newTree;
							act5.propertyMetadata = newArray.metadata;
							act5.oldIndex = i6;
							act5.newIndex = i6;
							act5.actionType = DescriptionTreeActionType.ReplaceItem;
							act5.oldValue = oldArr[i6];
							act5.newValue = newArr[i6];
							actions.add(act5);
						}
					} else {
						if (!Base.equalsStatic(oldItem, newItem)) {
							let act6 = new DescriptionTreeAction();
							act6.propertyName = newArray.propertyName;
							act6.targetNode = oldTree;
							act6.currentNode = newTree;
							act6.propertyMetadata = newArray.metadata;
							act6.oldIndex = i6;
							act6.newIndex = i6;
							act6.actionType = DescriptionTreeActionType.ReplaceItem;
							act6.oldValue = oldArr[i6];
							act6.newValue = newArr[i6];
							actions.add(act6);
						}
					}
				}
			}
		}
	}
	private static arraysEquivalent(value1: any, value2: any): boolean {
		let arr1 = <any[]>value1;
		let arr2 = <any[]>value2;
		if (arr1 == null && arr2 == null) {
			return true;
		}
		if ((arr1 != null && arr2 == null) || (arr1 == null && arr2 != null)) {
			return false;
		}
		if (arr1.length != arr2.length) {
			return false;
		}
		for (let i = 0; i < arr1.length; i++) {
			let arr1Item = arr1[i];
			let arr2Item = arr2[i];
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr1Item) !== null && !(typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr2Item) !== null)) {
				return false;
			}
			if (!(typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr1Item) !== null) && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr2Item) !== null) {
				return false;
			}
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr1Item) !== null) {
				let arr1Node = (<DescriptionTreeNode>arr1Item);
				let arr2Node = (<DescriptionTreeNode>arr2Item);
				if (arr1Node.type != arr2Node.type) {
					return false;
				}
				let arr1Name = arr1Node.has("Name") ? <string>arr1Node.get("Name").value : null;
				let arr2Name = arr2Node.has("Name") ? <string>arr2Node.get("Name").value : null;
				if (!Base.equalsStatic(arr1Name, arr2Name)) {
					return false;
				}
			} else {
				if (!Base.equalsStatic(arr1Item, arr2Item)) {
					return false;
				}
			}
		}
		return true;
	}
	private static getNonArrayProperties(tree: DescriptionTreeNode): List$1<DescriptionPropertyValue> {
		let ret: List$1<DescriptionPropertyValue> = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		let items = tree.items();
		for (let i = 0; i < items.count; i++) {
			let item = items._inner[i];
			if (item.metadata != null && item.metadata.knownType != TypeDescriptionWellKnownType.Array && item.metadata.knownType != TypeDescriptionWellKnownType.Collection) {
				ret.add(item);
			}
		}
		return ret;
	}
	private static getArrayProperties(tree: DescriptionTreeNode): List$1<DescriptionPropertyValue> {
		let ret: List$1<DescriptionPropertyValue> = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
		let items = tree.items();
		for (let i = 0; i < items.count; i++) {
			let item = items._inner[i];
			if (item.metadata != null && (item.metadata.knownType == TypeDescriptionWellKnownType.Array || item.metadata.knownType == TypeDescriptionWellKnownType.Collection)) {
				ret.add(item);
			}
		}
		return ret;
	}
}


