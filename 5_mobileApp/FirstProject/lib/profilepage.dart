import 'package:flutter/material.dart';

import 'components/app_drawer.dart';
import 'components/mytextfield.dart';

class ProfilePage extends StatelessWidget {
  const ProfilePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: const AppDrawer(),

      appBar: AppBar(
        title: const Text('First App - profile page'),
        backgroundColor: Colors.deepPurple,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Form(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // استخدام الدالة المساعدة لحقل اسم المستخدم
                const MyTextField(
                  label: 'Edit User Name',
                  hint: 'Username',
                ),
                const SizedBox(height: 20),

                // استخدام الدالة المساعدة لحقل الإيميل
                const MyTextField(
                  label: 'Edit Email',
                  hint: 'Email',
                ),
                const SizedBox(height: 20),

                // استخدام الدالة المساعدة لحقل كلمة المرور (مع إخفاء الحروف)
                const MyTextField(
                  label: 'Enter password',
                  hint: 'Password',
                  obscureText: true, // لإخفاء الحروف تلقائياً
                ),
                const SizedBox(height: 30),

                // زر التحديث
                SizedBox(
                  width: double.infinity,
                  child: ElevatedButton(
                    onPressed: () {},
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.purple,
                      padding: const EdgeInsets.symmetric(vertical: 15),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                      ),
                    ),
                    child: const Text(
                      'Update',
                      style: TextStyle(
                        fontSize: 18,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),
              ],
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