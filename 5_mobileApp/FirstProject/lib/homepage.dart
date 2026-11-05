import 'package:flutter/material.dart';
import 'components/app_drawer.dart';
import 'ProfilePage.dart';

class HomePage extends StatelessWidget {
  final String username;
  final int userId;
  final String email;

  // استقبال المتغيرات عبر الـ Constructor (الخيار الثاني المرن)
  const HomePage({
    this.username = 'User',
    this.userId = 0,
    this.email = 'user@email.com',
    super.key,
  });


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // تمرير بيانات المستخدم للـ AppDrawer لكي تظهر في القائمة الجانبية
      drawer: AppDrawer(
        username: username,
        email: email,
      ),
      appBar: AppBar(
        title: Text('Welcome, $username'), // عرض اسم المستخدم في العنوان إن أردتِ
        backgroundColor: Colors.deepPurple,
      ),
      body: Container(
        width: double.infinity,
        height: double.infinity,
        decoration: const BoxDecoration(
          gradient: RadialGradient(
            colors: [Colors.deepPurple, Colors.black26],
            radius: 0.9,
          ),
        ),
        child: Center(
          child: Text(
            'Welcome \n $username ',
            textAlign: TextAlign.center,
            style: const TextStyle(
              fontSize: 26,
              color: Colors.white,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
      ),
      bottomNavigationBar: Container(
        color: Colors.deepPurple,
        height: 80,
      ),
    );
  }
}