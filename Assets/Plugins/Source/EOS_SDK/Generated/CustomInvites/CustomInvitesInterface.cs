// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.CustomInvites
{
	public sealed partial class CustomInvitesInterface : Handle
	{
		public CustomInvitesInterface()
		{
		}

		public CustomInvitesInterface(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyCustomInviteAccepted" /> API.
		/// </summary>
		public const int AddnotifycustominviteacceptedApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyCustomInviteReceived" /> API.
		/// </summary>
		public const int AddnotifycustominvitereceivedApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="FinalizeInvite" /> API.
		/// </summary>
		public const int FinalizeinviteApiLatest = 1;

		/// <summary>
		/// Maximum size of the custom invite payload string
		/// </summary>
		public const int MaxPayloadLength = 500;

		/// <summary>
		/// The most recent version of the <see cref="SendCustomInvite" /> API.
		/// </summary>
		public const int SendcustominviteApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="SetCustomInvite" /> API.
		/// </summary>
		public const int SetcustominviteApiLatest = 1;

		/// <summary>
		/// Register to receive notifications when a Custom Invite for any logged in local users is accepted via the Social Overlay
		/// @note must call <see cref="RemoveNotifyCustomInviteAccepted" /> to remove the notification
		/// </summary>
		/// <param name="options">Structure containing information about the request.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate.</param>
		/// <param name="notificationFn">A callback that is fired when a Custom Invite is accepted via the Social Overlay.</param>
		/// <returns>
		/// handle representing the registered callback
		/// </returns>
		public ulong AddNotifyCustomInviteAccepted(AddNotifyCustomInviteAcceptedOptions options, object clientData, OnCustomInviteAcceptedCallback notificationFn)
		{
			var optionsAddress = System.IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyCustomInviteAcceptedOptionsInternal, AddNotifyCustomInviteAcceptedOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnCustomInviteAcceptedCallbackInternal(OnCustomInviteAcceptedCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = Bindings.EOS_CustomInvites_AddNotifyCustomInviteAccepted(InnerHandle, optionsAddress, clientDataAddress, notificationFnInternal);

			Helper.TryMarshalDispose(ref optionsAddress);

			Helper.TryAssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Register to receive notifications when a Custom Invite for any logged in local users is received
		/// @note must call <see cref="RemoveNotifyCustomInviteReceived" /> to remove the notification
		/// </summary>
		/// <param name="options">Structure containing information about the request.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate.</param>
		/// <param name="notificationFn">A callback that is fired when a Custom Invite is received.</param>
		/// <returns>
		/// handle representing the registered callback
		/// </returns>
		public ulong AddNotifyCustomInviteReceived(AddNotifyCustomInviteReceivedOptions options, object clientData, OnCustomInviteReceivedCallback notificationFn)
		{
			var optionsAddress = System.IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyCustomInviteReceivedOptionsInternal, AddNotifyCustomInviteReceivedOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnCustomInviteReceivedCallbackInternal(OnCustomInviteReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = Bindings.EOS_CustomInvites_AddNotifyCustomInviteReceived(InnerHandle, optionsAddress, clientDataAddress, notificationFnInternal);

			Helper.TryMarshalDispose(ref optionsAddress);

			Helper.TryAssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Signal that the title has completed processing a received Custom Invite, and that it should be cleaned up internally and in the Overlay
		/// </summary>
		/// <param name="options">Structure containing information about the request.</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation completes successfully
		/// <see cref="Result.InvalidParameters" /> if any of the option values are incorrect
		/// </returns>
		public Result FinalizeInvite(FinalizeInviteOptions options)
		{
			var optionsAddress = System.IntPtr.Zero;
			Helper.TryMarshalSet<FinalizeInviteOptionsInternal, FinalizeInviteOptions>(ref optionsAddress, options);

			var funcResult = Bindings.EOS_CustomInvites_FinalizeInvite(InnerHandle, optionsAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			return funcResult;
		}

		/// <summary>
		/// Unregister from receiving notifications when a Custom Invite for any logged in local users is accepted via the Social Overlay
		/// </summary>
		/// <param name="inId">Handle representing the registered callback</param>
		public void RemoveNotifyCustomInviteAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);

			Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteAccepted(InnerHandle, inId);
		}

		/// <summary>
		/// Unregister from receiving notifications when a Custom Invite for any logged in local users is received
		/// </summary>
		/// <param name="inId">Handle representing the registered callback</param>
		public void RemoveNotifyCustomInviteReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);

			Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteReceived(InnerHandle, inId);
		}

		/// <summary>
		/// Sends a Custom Invite that has previously been initialized via SetCustomInvite to a group of users.
		/// </summary>
		/// <param name="options">Structure containing information about the request.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="completionDelegate">A callback that is fired when the operation completes, either successfully or in error</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the query completes successfully
		/// <see cref="Result.InvalidParameters" /> if any of the options values are incorrect
		/// <see cref="Result.TooManyRequests" /> if the number of allowed queries is exceeded
		/// <see cref="Result.NotFound" /> if SetCustomInvite has not been previously successfully called for this user
		/// </returns>
		public void SendCustomInvite(SendCustomInviteOptions options, object clientData, OnSendCustomInviteCallback completionDelegate)
		{
			var optionsAddress = System.IntPtr.Zero;
			Helper.TryMarshalSet<SendCustomInviteOptionsInternal, SendCustomInviteOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnSendCustomInviteCallbackInternal(OnSendCustomInviteCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			Bindings.EOS_CustomInvites_SendCustomInvite(InnerHandle, optionsAddress, clientDataAddress, completionDelegateInternal);

			Helper.TryMarshalDispose(ref optionsAddress);
		}

		/// <summary>
		/// Initializes a Custom Invite with a specified payload in preparation for it to be sent to another user or users.
		/// </summary>
		/// <param name="options">Structure containing information about the request.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="completionDelegate">A callback that is fired when the operation completes, either successfully or in error</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation completes successfully
		/// <see cref="Result.InvalidParameters" /> if any of the options values are incorrect
		/// </returns>
		public Result SetCustomInvite(SetCustomInviteOptions options)
		{
			var optionsAddress = System.IntPtr.Zero;
			Helper.TryMarshalSet<SetCustomInviteOptionsInternal, SetCustomInviteOptions>(ref optionsAddress, options);

			var funcResult = Bindings.EOS_CustomInvites_SetCustomInvite(InnerHandle, optionsAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			return funcResult;
		}

		[MonoPInvokeCallback(typeof(OnCustomInviteAcceptedCallbackInternal))]
		internal static void OnCustomInviteAcceptedCallbackInternalImplementation(System.IntPtr data)
		{
			OnCustomInviteAcceptedCallback callback;
			OnCustomInviteAcceptedCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnCustomInviteAcceptedCallback, OnCustomInviteAcceptedCallbackInfoInternal, OnCustomInviteAcceptedCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnCustomInviteReceivedCallbackInternal))]
		internal static void OnCustomInviteReceivedCallbackInternalImplementation(System.IntPtr data)
		{
			OnCustomInviteReceivedCallback callback;
			OnCustomInviteReceivedCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnCustomInviteReceivedCallback, OnCustomInviteReceivedCallbackInfoInternal, OnCustomInviteReceivedCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnSendCustomInviteCallbackInternal))]
		internal static void OnSendCustomInviteCallbackInternalImplementation(System.IntPtr data)
		{
			OnSendCustomInviteCallback callback;
			SendCustomInviteCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnSendCustomInviteCallback, SendCustomInviteCallbackInfoInternal, SendCustomInviteCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}
	}
}