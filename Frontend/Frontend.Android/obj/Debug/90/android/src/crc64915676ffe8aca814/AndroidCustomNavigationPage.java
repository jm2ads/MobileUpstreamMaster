package crc64915676ffe8aca814;


public class AndroidCustomNavigationPage
	extends crc64720bb2db43a66fe9.NavigationPageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"";
		mono.android.Runtime.register ("Frontend.Droid.Implementations.AndroidCustomNavigationPage, Frontend.Android", AndroidCustomNavigationPage.class, __md_methods);
	}


	public AndroidCustomNavigationPage (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == AndroidCustomNavigationPage.class)
			mono.android.TypeManager.Activate ("Frontend.Droid.Implementations.AndroidCustomNavigationPage, Frontend.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public AndroidCustomNavigationPage (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == AndroidCustomNavigationPage.class)
			mono.android.TypeManager.Activate ("Frontend.Droid.Implementations.AndroidCustomNavigationPage, Frontend.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public AndroidCustomNavigationPage (android.content.Context p0)
	{
		super (p0);
		if (getClass () == AndroidCustomNavigationPage.class)
			mono.android.TypeManager.Activate ("Frontend.Droid.Implementations.AndroidCustomNavigationPage, Frontend.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
