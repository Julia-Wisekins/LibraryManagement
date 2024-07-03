namespace LibraryManagementBackend
{
    /// <summary>
    /// What kind of notification is sent to the <see cref="IManagementMediator"/>
    /// </summary>
    enum BookEvent
    {
        Borrowed,
        Returned,
        Disposed,
    }
}
