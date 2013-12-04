using System;
using System.Collections;
using System.Collections.Generic;

namespace NotificationCenter {
	public class NSNotification {
		private readonly String _name;
		private readonly System.Object _object;
		private readonly Hashtable _userInfo;
		
		private NSNotification(String aName, System.Object anObject, Hashtable userInfo = null) {
			_name = aName;
			_object = anObject;
			_userInfo = userInfo;
	    }
	
		
		
		// Returns the name of the notification.
		//
		// Return Value
		// 		The name of the notification. Typically you use this method to find out what kind of notification you are dealing with when you receive a notification.
		//
		// Special Considerations
		// 		Notification names can be any string. To avoid name collisions, you might want to use a prefix that’s specific to your application.
		public String name {
			get { return _name; }
		}
		
		// Returns the object associated with the notification.
		//
		// Return Value
		// The object associated with the notification. This is often the object that posted this notification. It may be nil.
		//
		// Typically you use this method to find out what object a notification applies to when you receive a notification.
		public System.Object obj {
			get { return _object; }
		}
		
		// Returns the user info associated with the notification.
		//
		// Return Value
		//		Returns the user information dictionary associated with the receiver. May be nil.
		//
		//		The user information dictionary stores any additional objects that objects receiving the notification might use.
		public Hashtable userInfo {
			get { return _userInfo; }
		}
		
		// Returns a new notification object with a specified name and object.
		//
		// Parameters
		// 		aName
		// 			The name for the new notification. May not be nil.
		// 		anObject
		// 			The object for the new notification.
		public static NSNotification notificationWithNameObject(String aName, System.Object anObject) {
			return (new NSNotification(aName, anObject));
		}
		
		public static NSNotification notificationWithNameObjectUserInfo(String aName, System.Object anObject, Hashtable userInfo) {
			return (new NSNotification(aName, anObject, userInfo));
		}
	}
	
	
	public class NSNotificationCenter {
		public delegate void NotificationSelector(NSNotification aNotification); // ±= function pointer
		
		private System.Object observerForPredicate = null;
		private System.Object senderForPredicate = null;
		
		System.Object objectLock = new System.Object();
		
		private class ObserverSelectorSender {
			private System.Object _observer;
			private NotificationSelector _selector;
			private System.Object _sender;
			
			public ObserverSelectorSender (System.Object anObserver, NotificationSelector aSelector, System.Object aSender=null) {
				_observer = anObserver;
				_selector = aSelector;
				_sender = aSender;
			}
			
			public System.Object observer {
				get { return _observer; }
			}
			
			public NotificationSelector selector {
				get { return _selector; }
			}
			
			public System.Object sender {
				get { return _sender; }
			}
		}
	
		private static NSNotificationCenter singleton;
		private readonly Hashtable dict;
		
		private NSNotificationCenter()  {
	        dict=new Hashtable();
	     }
		
		private bool matchNotificationObserverAndNotificationSender(ObserverSelectorSender oss) {
			bool matchinObservers = (observerForPredicate == oss.observer);
			bool matchingSenders = ((senderForPredicate==null) || (senderForPredicate==oss.sender));
			return (matchinObservers && matchingSenders);
	    }
		
		
		// Returns the process’s default notification center. (Always the same in current implementation)
		//
		// Return Value
		// The current process’s default notification center, which is used for system notifications.
		public  static NSNotificationCenter defaultCenter { // singleton pattern
			get { return singleton ?? (singleton = new NSNotificationCenter()); }
		}
		
		// Adds an entry to the receiver’s dispatch table with a notification selector (function pointer) and optional criteria: notification name and sender.
		//
		// Parameters
		// 		notificationObserver
		// 			Object registering as an observer. This value must not be null.
		// 		notificationSelector
		// 			Selector that specifies the message the receiver sends notificationObserver to notify it of the notification posting.
		// 			The method specified by notificationSelector must have one and only one argument (an instance of NSNotification).
		// 			This value must not be null.
		// 		notificationName
		// 			The name of the notification for which to register the observer; that is, only notifications with this name are delivered to the observer.
		// 			If you pass nil, the notification center doesn’t use a notification’s name to decide whether to deliver it to the observer.
		// 		notificationSender
		// 			The object whose notifications the observer wants to receive; that is, only notifications sent by this sender are delivered to the observer.
		// 			If you pass nil, the notification center doesn’t use a notification’s sender to decide whether to deliver it to the observer.
		public void addObserverSelectorNameObject (System.Object notificationObserver, NotificationSelector notificationSelector, String notificationName, System.Object notificationSender) {
			// check parameters
			if (notificationObserver==null) throw new ArgumentNullException(@"notificationObserver");
			if (notificationSelector==null) throw new ArgumentNullException(@"notificationSelector");
			if (string.IsNullOrEmpty(notificationName)) throw new ArgumentNullException(@"notificationName");
			//if (notificationSelector.Target!=notificationObserver) throw new ArgumentException(@"notificationSelector doesn't belong to notificationObserver");
	
			lock (objectLock) {  // ensure code is reentrant
				ObserverSelectorSender os= new ObserverSelectorSender(notificationObserver, notificationSelector, notificationSender);
				List<ObserverSelectorSender> list = (List<ObserverSelectorSender>)dict[notificationName];
				if(list==null) {
					list = new List<ObserverSelectorSender> (); 
					dict.Add(notificationName, list);
				}
				list.Add(os);
			}
		}
		
		// Creates a notification with a given name and sender and posts it to the receiver.
		//
		// Parameters
		//		notificationName
		//			The name of the notification.
		//	notificationSender
		//		The object posting the notification.
		public void postNotificationNameObject (String notificationName, System.Object notificationSender) {
			lock (objectLock) {  // ensure code is reentrant
				List<ObserverSelectorSender> list = (List<ObserverSelectorSender>)dict[notificationName];
				if(list!=null) {
					NSNotification not = NSNotification.notificationWithNameObject(notificationName, notificationSender); 
					foreach (ObserverSelectorSender os in list) {
						if ((os.sender==null) || (os.sender==notificationSender)) os.selector(not);
					}
				}
			}
		}
		
		// Creates a notification with a given name, sender, and information and posts it to the receiver.
		//
		// Parameters
		//		notificationName
		//			The name of the notification.
		//		notificationSender
		//			The object posting the notification.
		//		userInfo
		//			Information about the the notification. May be nil.
		//
		// Note : This method is the preferred method for posting notifications.
		public void postNotificationNameObjectUserInfo (String notificationName, System.Object notificationSender, Hashtable userInfo)  {
			lock (objectLock) {  // ensure code is reentrant
				List<ObserverSelectorSender> list = (List<ObserverSelectorSender>)dict[notificationName];
				if(list!=null) {
					NSNotification not = NSNotification.notificationWithNameObjectUserInfo(notificationName, notificationSender, userInfo); 
					foreach (ObserverSelectorSender os in list) {
						if ((os.sender==null) || (os.sender==notificationSender)) os.selector(not);
					}
				}
			}
		}
		
		// Removes matching entries from the receiver’s dispatch table.
		//
		// Parameters
		// 		notificationObserver
		// 			Observer to remove from the dispatch table. Specify an observer to remove only entries for this observer. Must not be nil, or message will have no effect.
		// 		notificationName
		// 			Name of the notification to remove from dispatch table. Specify a notification name to remove only entries that specify this notification name. When nil, the receiver does not use notification names as criteria for removal.
		// 		notificationSender
		// 			Sender to remove from the dispatch table. Specify a notification sender to remove only entries that specify this sender. When nil, the receiver does not use notification senders as criteria for removal.
		public void removeObserverNameObject (System.Object notificationObserver, String notificationName, System.Object notificationSender) {
			if (notificationObserver==null) return;
	
			lock (objectLock) {  // ensure code is reentrant
				observerForPredicate = notificationObserver;
				senderForPredicate = notificationSender;
				
				ArrayList listsToDelete = new ArrayList();
				foreach (String k in dict.Keys) {
					if ((notificationName==null) || (notificationName==k)) {
						List<ObserverSelectorSender> list = (List<ObserverSelectorSender>)dict[k];
						list.RemoveAll (matchNotificationObserverAndNotificationSender);
						if (list.Count==0) listsToDelete.Add (k); 
					}
				}
				
				notificationObserver = null;
				senderForPredicate = null;
				
				foreach ( String k in listsToDelete ) dict.Remove (k); // remove empty lists
			}		
			
		}
	
		// Removes all the entries specifying a given observer from the receiver’s dispatch table.
		// (unregister notificationObserver for all notifications)
		//
		// Parameters
		//		notificationObserver
		//			The observer to remove. Must not be nil.
		public void removeObserver (System.Object notificationObserver) {
			// check parameters
			if (notificationObserver==null) throw new ArgumentNullException(@"notificationObserver");
			
			removeObserverNameObject(notificationObserver, null, null);
		}
	}
}
